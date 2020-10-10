using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Hazel;
using Hazel.Udp;
using Impostor.Server.Net.Messages;
using Impostor.Shared;
using Impostor.Shared.Innersloth;
using Impostor.Shared.Innersloth.Enums;
using Impostor.Shared.Innersloth.GameData;
using Impostor.Shared.Innersloth.InnerNetComponents;
using Impostor.Shared.Innersloth.Messages;
using Impostor.Shared.Innersloth.RpcCommands;
using Newtonsoft.Json;
using PcapDotNet.Core;
using PcapDotNet.Packets;

namespace Impostor.Tools.Proxy
{
    internal static class Program
    {
        static int gamedataId = 0;
        static int allId = 0;

        private const string DeviceName = "Network adapter 'Intel(R) Ethernet Connection I217-V' on local host";

        public static bool LogNotConsumed = true;
        public static bool LogMoves = false;
        public static bool LogRPC = false;
        public static bool LogChat = false;
        public static bool SaveMessages = false;

        public static bool BreakOnGameStart = true;
        public static bool BreakOnGameEnd = true;
        public static bool BreakOnMettingStart = true;
        public static bool BreakOnMeetingEnd = true;


        public static Dictionary<int, PlayerState> players = new Dictionary<int, PlayerState>();

        private static void Main(string[] args)
        {
            Directory.CreateDirectory("all");
            //foreach (var f in Directory.EnumerateFiles("spawn"))
            //{
            //    var fi = new FileInfo(f);
            //    var send = fi.Name.Contains("send");

            //    var body = File.ReadAllBytes(f);
            //    var gd = new GameData()
            //    {
            //        body = body,
            //        type = (byte)GameDataType.Spawn,
            //    };
            //    HandleGameData(gd, send);
            //}

            //Regex idregex = new Regex("data_(\\d+)");
            //foreach (var f in Directory.EnumerateFiles("gamedata").OrderBy(p => int.Parse(idregex.Match(p).Groups[1].Value)))
            //{
            //    var fi = new FileInfo(f);
            //    var send = fi.Name.Contains("send");

            //    var body = File.ReadAllBytes(f);
            //    var ms = new MemoryStream(body);
            //    var reader = new HazelBinaryReader(ms);
            //    var gamedata = GameData.Deserialize(reader);
            //    HandleGameData(gamedata, send);

            //    if (reader.GetBytesLeft() > 0)
            //    {
            //        //throw new Exception();
            //    }
            //}

            Regex idregex = new Regex("_(\\d+)_(\\d+)");
            foreach (var f in Directory.EnumerateFiles("all").OrderBy(p => int.Parse(idregex.Match(p).Groups[2].Value)))
            {
                var fi = new FileInfo(f);
                var send = fi.Name.Contains("send");

                var body = File.ReadAllBytes(f);
                var reader = MessageReader.Get(body);
                reader.Tag = byte.Parse(idregex.Match(fi.Name).Groups[1].Value);

                if (send)
                {
                    HandleToServer("file", reader);
                }
                else
                {
                    HandleToClient("file", reader);
                }
            }



            var devices = LivePacketDevice.AllLocalMachine;
            if (devices.Count == 0)
            {
                Console.WriteLine("No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            var device = devices.FirstOrDefault(x => x.Description.Contains(DeviceName));
            if (device == null)
            {
                Console.WriteLine("Unable to find configured device.");
                return;
            }

            using (var communicator = device.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                using (var filter = communicator.CreateFilter("udp and port 22023"))
                {
                    communicator.SetFilter(filter);
                }

                communicator.ReceivePackets(0, PacketHandler);
            }
        }

        private static void PacketHandler(Packet packet)
        {
            var ip = packet.Ethernet.IpV4;
            var ipSrc = ip.Source.ToString();
            var udp = ip.Udp;
            var isSent = ipSrc.StartsWith("192.");
            // True if this is our own packet.
            using (var stream = udp.Payload.ToMemoryStream())
            {
                var messages = GetMessages(stream);

                foreach (var item in messages)
                {
                    var tag = item.First();
                    var reader = MessageReader.Get(item.Skip(1).ToArray());
                    reader.Tag = tag;
                    if (reader.Position >= reader.Length)
                    {
                        break;
                    }

                    if (isSent)
                    {
                        HandleToServer(ipSrc, reader);
                    }
                    else
                    {
                        HandleToClient(ipSrc, reader);
                    }

                    if (reader.Position < reader.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("- Did not consume all bytes.");
                    }
                }
            }
        }

        private static List<byte[]> GetMessages(MemoryStream stream)
        {
            var buffers = new List<byte[]>();

            HazelBinaryReader reader = new HazelBinaryReader(stream);

            var header = reader.ReadByte();
            if (header == (byte)UdpSendOption.Acknowledgement ||
                 header == (byte)UdpSendOption.Ping ||
                 header == (byte)UdpSendOption.Hello ||
                 header == (byte)UdpSendOption.Disconnect)
            {
                return buffers;
            }

            if (header == (byte)SendOption.Reliable)
            {
                var ack = reader.ReadInt16();
            }

            while (reader.HasBytesLeft())
            {
                int length = reader.ReadInt16();
                var data = reader.ReadBytes(length + 1);
                buffers.Add(data);
            }
            
            return buffers;
        }

        private static void HandleToClient(string source, MessageReader packet)
        {
            var messageType = (MessageType)packet.Tag;
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine($"{source,-15} Client received: {packet.Tag,-2} {messageType}");

            try
            {
                var reader = packet.GetHazelReader();
                var body = reader.PeekToEnd();
                if (SaveMessages)
                {
                    File.WriteAllBytes(Path.Combine("all", $"recv_{packet.Tag}_{allId++}.bin"), body);
                }
                
                switch (messageType)
                {
                    case MessageType.ReselectServer:
                        break;
                    case MessageType.Redirect:
                        break;
                    case MessageType.HostGame:
                        var host = HostGameResponse.Deserialize(reader);
                        DumpToConsole(host);
                        break;
                    case MessageType.JoinGame:
                        var join = JoinGameResponse.Deserialize(reader);
                        DumpToConsole(join);
                        break;
                    case MessageType.GameData:
                        var gamedata = GameData.Deserialize(reader);
                        foreach (var item in gamedata)
                        {
                            HandleGameData(item, false);
                        }

                        //Directory.CreateDirectory("gamedata");
                        //File.WriteAllBytes(Path.Combine("gamedata", $"recv_data_{gamedataId++}.bin"), body);
                        break;
                    case MessageType.GameDataTo:
                        var gamedatato = GameDataTo.Deserialize(reader);
                        foreach (var item in gamedatato)
                        {
                            HandleGameDataTo(item, false);
                        }
                        break;
                    case MessageType.JoinedGame:
                        var joined = JoinedGame.Deserialize(reader);
                        DumpToConsole(joined);
                        break;
                    case MessageType.AlterGame:
                        var alter = AlterGameResponse.Deserialize(reader);
                        DumpToConsole(alter);
                        break;
                    case MessageType.GetGameListV2:
                        var gamelist = GetGameListV2Response.Deserialize(reader);
                        DumpToConsole(gamelist);
                        break;

                    case MessageType.RemovePlayer:
                        var removeplayer = RemovePlayerResponse.Deserialize(reader);
                        DumpToConsole(removeplayer);
                        break;
                    case MessageType.StartGame:
                        var start = StartGame.Deserialize(reader);
                        DumpToConsole(start);
                        break;
                    case MessageType.EndGame:
                        var end = EndGame.Deserialize(reader);
                        DumpToConsole(end);
                        break;
                    default:
                        Console.WriteLine($"Unhandled Message: {messageType} size: {body.Length}");
                        return;
                }

                if (reader.GetBytesLeft() > 0 && LogNotConsumed)
                {
                    Console.WriteLine($"[{messageType}]{reader.GetBytesLeft()} bytes not cunsumed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling ToClient: " + ex.Message);
            }
        }

        private static void HandleToServer(string source, MessageReader packet)
        {
            var messageType = (MessageType)packet.Tag;
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine($"{source,-15} Server received: {packet.Tag,-2} {messageType}");

            try
            {
                var reader = packet.GetHazelReader();
                var body = reader.PeekToEnd();
                if (SaveMessages)
                {
                    File.WriteAllBytes(Path.Combine("all", $"send_{packet.Tag}_{allId++}.bin"), body);
                }
                
                switch (messageType)
                {
                    case MessageType.HostGame:
                        var host = HostGameRequest.Deserialize(reader);
                        DumpToConsole(host);
                        break;
                    case MessageType.JoinGame:
                        var join = JoinGameRequest.Deserialize(reader);
                        DumpToConsole(join);
                        break;
                    case MessageType.GameData:
                        var gamedata = GameData.Deserialize(reader);
                        foreach (var item in gamedata)
                        {
                            HandleGameData(item, true);
                        }
                        //Directory.CreateDirectory("gamedata");
                        //File.WriteAllBytes(Path.Combine("gamedata", $"send_data_{gamedataId++}.bin"), body);
                        break;
                    case MessageType.GameDataTo:
                        var gamedatato = GameDataTo.Deserialize(reader);
                        foreach (var item in gamedatato)
                        {
                            HandleGameDataTo(item, true);
                        }
                        break;
                    case MessageType.GetGameListV2:
                        var gamelistrequest = GetGameListV2Request.Deserialize(reader);
                        DumpToConsole(gamelistrequest);
                        break;
                    case MessageType.RemovePlayer:
                        var removeplayer = RemovePlayerRequest.Deserialize(reader);
                        DumpToConsole(removeplayer);
                        break;
                    default:
                        Console.WriteLine($"Unhandled Message: {messageType} size: {body.Length}");
                        return;
                }

                if (reader.GetBytesLeft() > 0 && LogNotConsumed)
                {
                    Console.WriteLine($"[{messageType}]{reader.GetBytesLeft()} bytes not cunsumed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling ToServer: " + ex.Message);
            }
        }

        static int spawn_id = 0;

        static void HandleGameData(GameData data, bool send)
        {
            var ms = new MemoryStream(data.body);
            var reader = new HazelBinaryReader(ms);
            var datatype = (GameDataType)data.type;

            HandleGameData(reader, datatype, send);
        }

        static void HandleGameDataTo(GameDataTo data, bool send)
        {
            var ms = new MemoryStream(data.body);
            var reader = new HazelBinaryReader(ms);
            var datatype = (GameDataType)data.type;

            HandleGameData(reader, datatype, send);
        }

        static void HandleGameData(HazelBinaryReader reader, GameDataType datatype, bool send)
        {
            var dir = send ? "SEND" : "RECV";
            switch (datatype)
            {
                case GameDataType.RpcCall:
                    var RPC = RpcCall.Deserialize(reader);
                    var reciver = EntityTracker.Get(RPC.targetNetId);
                    if (LogRPC)
                    {
                        Console.WriteLine($"[RPC][{RPC.callId}]sent to {reciver?.GetType()?.Name ?? "unknown"} size: {reader.GetBytesLeft()}");
                    }
                    if (reciver != null)
                    {
                        reciver.HandleRpcCall(RPC.callId, reader);
                    }
                    //DumpToConsole(RPC, LogRPC);
                    //Console.WriteLine($"RPC for type: {RPC} size: {data.body.Length}");
                    switch (RPC.callId)
                    {
                        case RpcCalls.CheckColor:
                            var CheckColor = CmdCheckColor.Deserialize(reader);
                            DumpToConsole(CheckColor, LogRPC);
                            break;
                        case RpcCalls.CheckName:
                            var CheckName = CmdCheckName.Deserialize(reader);
                            DumpToConsole(CheckName, LogRPC);
                            break;

                        case RpcCalls.CloseDoorsOfType:
                            var CloseDoorsOfType = RpcCloseDoorsOfType.Deserialize(reader);
                            DumpToConsole(CloseDoorsOfType, LogRPC);
                            break;
                        case RpcCalls.EnterVent:
                            var EnterVent = RpcEnterVent.Deserialize(reader);
                            DumpToConsole(EnterVent, LogRPC);
                            break;
                        case RpcCalls.ExitVent:
                            var ExitVent = RpcExitVent.Deserialize(reader);
                            DumpToConsole(ExitVent, LogRPC);
                            break;
                        case RpcCalls.Exiled:
                            var exileid = reader.ReadPackedInt32();
                            Console.WriteLine("Exile id: " + exileid);
                            break;
                        case RpcCalls.SendChat:
                            var chat = RpcSendChat.Deserialize(reader);
                            LogChatToConsole(RPC.targetNetId.ToString(), chat.text);
                            break;
                        case RpcCalls.SendChatNote:
                            var chatnote = RpcSendChatNote.Deserialize(reader);
                            LogChatToConsole("server", "SendChatNote");
                            break;

                        case RpcCalls.SetColor:
                            var SetColor = RpcSetColor.Deserialize(reader);
                            DumpToConsole(SetColor, LogRPC);
                            break;
                        case RpcCalls.SetSkin:
                            var skin = RpcSetSkin.Deserialize(reader);
                            DumpToConsole(skin, LogRPC);
                            break;
                        case RpcCalls.SetHat:
                            var hat = RpcSetHat.Deserialize(reader);
                            Console.WriteLine($"Set hat: [{(int)hat.hatId}]{hat.hatId}");
                            break;
                        case RpcCalls.SetPet:
                            var setpet = RpcSetPet.Deserialize(reader);
                            DumpToConsole(setpet, LogRPC);
                            break;
                        case RpcCalls.SetName:
                            var setname = RpcSetName.Deserialize(reader);
                            DumpToConsole(setname, LogRPC);
                            break;
                        case RpcCalls.SetTasks:
                            var SetTasks = RpcSetTasks.Deserialize(reader);
                            DumpToConsole(SetTasks, LogRPC);
                            break;
                        case RpcCalls.SetInfected:
                            var infected = RpcSetInfected.Deserialize(reader);
                            DumpToConsole(infected);
                            break;
                        case RpcCalls.SetScanner:
                            var SetScanner = RpcSetScanner.Deserialize(reader);
                            DumpToConsole(SetScanner, LogRPC);
                            break;

                        case RpcCalls.AddVote:
                            var addvote = CmdCastVote.Deserialize(reader);
                            DumpToConsole(addvote, LogRPC);
                            break;
                        case RpcCalls.PlayAnimation:
                            var anim = RpcPlayAnimation.Deserialize(reader);
                            DumpToConsole(anim, LogRPC);
                            break;
                        case RpcCalls.CastVote:
                            var castvote = CmdCastVote.Deserialize(reader);
                            DumpToConsole(castvote, LogRPC);
                            break;
                        case RpcCalls.CompleteTask:
                            var complete = RpcCompleteTask.Deserialize(reader);
                            DumpToConsole(complete, LogRPC);
                            break;
                       
                        case RpcCalls.MurderPlayer:
                            var MurderPlayer = RpcMurderPlayer.Deserialize(reader);
                            var player = EntityTracker.Get(MurderPlayer.netId);
                            DumpToConsole(MurderPlayer, LogRPC);
                            break;
                        case RpcCalls.RepairSystem:
                            var RepairSystem = RpcRepairSystem.Deserialize(reader);
                            DumpToConsole(RepairSystem, LogRPC);
                            break;
                        case RpcCalls.ReportDeadBody:
                            var ReportDeadBody = CmdReportDeadBody.Deserialize(reader);
                            DumpToConsole(ReportDeadBody, LogRPC);
                            break;

                        case RpcCalls.SnapTo:
                            var SnapTo = RpcSnapTo.Deserialize(reader);
                            DumpToConsole(SnapTo, LogRPC);
                            break;
                        case RpcCalls.StartMeeting:
                            var StartMeeting = RpcStartMeeting.Deserialize(reader);
                            DumpToConsole(StartMeeting, LogRPC);
                            break;
                        case RpcCalls.VotingComplete:
                            var VotingComplete = RpcVotingComplete.Deserialize(reader);
                            DumpToConsole(VotingComplete, LogRPC);
                            break;
                        case RpcCalls.SetStartCounter:
                            var startcounter = RpcSetStartCounter.Deserialize(reader);
                            DumpToConsole(startcounter, LogRPC);
                            break;
                        //Dont have a message body
                        case RpcCalls.Close:
                        //Currently not implemented
                        
                        case RpcCalls.SyncSettings:
                            var syncsettings = RpcSyncSettings.Deserialize(reader);
                            DumpToConsole(syncsettings, LogRPC);
                            break;

                        //ComponentSpecific - it contains diffrent data depending on what component it's inteded for
                        case RpcCalls.UpdateGameData:
                            var update = RpcUpdateGameData.Deserialize(reader);
                            DumpToConsole(update, LogRPC);
                            break;

                        default:
                            Console.WriteLine($"Unhandled RPC command: " + RPC.callId);
                            break;
                    }
                    if (reader.GetBytesLeft() > 0 && LogNotConsumed)
                    {
                        Console.WriteLine($"[{RPC.callId}]{reader.GetBytesLeft()} bytes not cunsumed");
                        reader.ReadBytesToEnd();
                    }
                    break;

                case GameDataType.Data:
                    var data = Data.Deserialize(reader);
                    InnerNetObject entity;
                    if (!EntityTracker.entities.TryGetValue(data.netId, out entity))
                    {
                        Console.WriteLine($"Entity missing for id: {data.netId} size: {data.data.Length}");
                        return;
                    }
                    if (!(entity is CustomNetworkTransform))
                    {
                        Console.WriteLine($"Recived Data for: {entity.GetType().Name} size: {data.data}");
                    }

                    entity.Deserialize(new HazelBinaryReader(data.data), false);

                    if (LogMoves && entity is CustomNetworkTransform move)
                    {
                        Console.WriteLine($"[{dir}]Move command player: {move.OwnerId:0000} seq: {move.seq:0000} pos: {move.position} delta: {move.velocity}");
                    }

                    break;

                case GameDataType.Spawn:
                    var spawn = Spawn.Deserialize(reader);
                    Console.WriteLine("Spawning: " + spawn.spawnId);
                    switch (spawn.spawnId)
                    {
                        case 1:
                            var meeting = new MeetingHud();
                            meeting.OwnerId = spawn.ownerId;
                            meeting.NetId = spawn.children[0].netId;
                            EntityTracker.Add(meeting);
                            break;
                        case 2:
                            var lobby = new LobbyBehaviour();
                            lobby.OwnerId = spawn.ownerId;
                            lobby.NetId = spawn.children[0].netId;
                            EntityTracker.Add(lobby);
                            break;
                        case 3:
                            var dummy = new GameDataComponent();
                            dummy.OwnerId = spawn.ownerId;
                            dummy.NetId = spawn.children[0].netId;
                            dummy.Deserialize(new HazelBinaryReader(spawn.children[0].body), true);
                            EntityTracker.Add(dummy);

                            var dummy2 = new DummyComponent();
                            dummy2.name = "gamedata.dummy1";
                            dummy2.OwnerId = spawn.ownerId;
                            dummy2.NetId = spawn.children[1].netId;
                            dummy2.Deserialize(new HazelBinaryReader(spawn.children[1].body), true);
                            EntityTracker.Add(dummy2);
                            break;
                        case 4://player character
                            var player = new PlayerControl();
                            player.dummy0 = new DummyComponent();
                            player.dummy0.name = "player.dummy0";
                            player.dummy0.OwnerId = spawn.ownerId;
                            player.dummy0.NetId = spawn.children[0].netId;
                            player.dummy0.Deserialize(new HazelBinaryReader(spawn.children[0].body), true);
                            EntityTracker.Add(player.dummy0);

                            player.dummy1 = new DummyComponent();
                            player.dummy1.name = "player.dummy1";
                            player.dummy1.OwnerId = spawn.ownerId;
                            player.dummy1.NetId = spawn.children[1].netId;
                            player.dummy1.Deserialize(new HazelBinaryReader(spawn.children[1].body), true);
                            EntityTracker.Add(player.dummy1);

                            player.transform = new CustomNetworkTransform();
                            player.transform.OwnerId = spawn.ownerId;
                            player.transform.NetId = spawn.children[2].netId;
                            player.transform.Deserialize(new HazelBinaryReader(spawn.children[2].body), true);
                            EntityTracker.Add(player.transform);
                            DumpToConsole(player);
                            break;
                        case 5://HeadQuarters
                        case 6://PlanetMap
                        case 7://AprilShipStatus
                        default:
                            Console.WriteLine($"Unhandled spawnid: {spawn.spawnId}");
                            break;
                    }
                    break;
                case GameDataType.SceneChange:
                    var scene = SceneChange.Deserialize(reader);
                    if (scene.sceneName == "OnlineGame")
                    {
                        //Starting game
                    }
                    DumpToConsole(scene);
                    break;
                case GameDataType.Despawn:
                    var despawn = Despawn.Deserialize(reader);
                    EntityTracker.entities.Remove(despawn.netId);
                    LogToConsole("Despawn Netid: " + despawn.netId);
                    break;
                case GameDataType.Ready:
                    var ready = Ready.Deserialize(reader);
                    Console.WriteLine($"Ready: " + ready.playerId);
                    break;
                case GameDataType.ChangeSettings:
                    var changesettings = ChangeSettings.Deserialize(reader);
                    DumpToConsole(changesettings);
                    break;
                default:
                    Console.WriteLine($"Unhandled Gamedatatype: {datatype}");
                    break;
            }

            if (reader.GetBytesLeft() > 0 && LogNotConsumed)
            {
                Console.WriteLine($"[{datatype}]{reader.GetBytesLeft()} bytes not cunsumed");
                reader.ReadBytesToEnd();
            }
        }

        static void LogChatToConsole(string sender, string message)
        {
            if (LogChat)
            {
                Console.WriteLine($"[CHAT][{sender}]{message}");
            }
        }

        static void LogToConsole(string message, bool shouldLog = true)
        {
            if (LogChat) Console.WriteLine(message);
        }

        static void DumpToConsole(object data, bool shouldLog = true)
        {
            if (shouldLog)
	        {
                Console.WriteLine(data.GetType().Name);
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
                Console.WriteLine(json);
	        }
        }
    }
}
