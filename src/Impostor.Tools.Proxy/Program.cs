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
using Impostor.Shared.Innersloth.Enums;
using Impostor.Shared.Innersloth.GameData;
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

        private const string DeviceName = "Network adapter 'Intel(R) Ethernet Connection I217-V' on local host";

        public static bool LogNotConsumed = false;
        public static bool LogMoves = false;
        public static bool LogRPC = false;


        public static Dictionary<int, PlayerState> players = new Dictionary<int, PlayerState>();

        private static void Main(string[] args)
        {

            foreach (var f in Directory.EnumerateFiles("spawn"))
            {
                var fi = new FileInfo(f);
                var send = fi.Name.Contains("send");

                var body = File.ReadAllBytes(f);
                var gd = new GameData()
                {
                    body = body,
                    type = (byte)GameDataType.Spawn,
                };
                HandleGameData(gd, send);
            }

            Regex idregex = new Regex("data_(\\d+)");
            foreach (var f in Directory.EnumerateFiles("gamedata").OrderBy(p => int.Parse(idregex.Match(p).Groups[1].Value)))
            {
                var fi = new FileInfo(f);
                var send = fi.Name.Contains("send");

                var body = File.ReadAllBytes(f);
                var ms = new MemoryStream(body);
                var reader = new HazelBinaryReader(ms);
                var gamedata = GameData.Deserialize(reader);
                HandleGameData(gamedata, send);

                if (reader.GetBytesLeft() > 0)
                {
                    //throw new Exception();
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

            // True if this is our own packet.
            using (var stream = udp.Payload.ToMemoryStream())
            {
                var reader = MessageReader.Get(stream.ToArray());
                if (reader.Buffer[0] == (byte)SendOption.Reliable)
                {
                    reader.Offset = 3;
                    reader.Length = udp.Payload.Length - 3;
                    reader.Position = 0;
                }
                else if (reader.Buffer[0] == (byte)UdpSendOption.Acknowledgement ||
                         reader.Buffer[0] == (byte)UdpSendOption.Ping ||
                         reader.Buffer[0] == (byte)UdpSendOption.Hello ||
                         reader.Buffer[0] == (byte)UdpSendOption.Disconnect)
                {
                    return;
                }
                else
                {
                    reader.Offset = 1;
                    reader.Length = udp.Payload.Length - 1;
                    reader.Position = 0;
                }

                var isSent = ipSrc.StartsWith("192.");

                while (true)
                {
                    if (reader.Position >= reader.Length)
                    {
                        break;
                    }

                    var message = reader.ReadMessage();
                    if (isSent)
                    {
                        HandleToServer(ipSrc, message);
                    }
                    else
                    {
                        HandleToClient(ipSrc, message);
                    }

                    if (message.Position < message.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("- Did not consume all bytes.");
                    }
                }
            }
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

                switch (messageType)
                {
                    case MessageType.ReselectServer:
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
                        HandleGameData(gamedata, false);

                        Directory.CreateDirectory("gamedata");
                        File.WriteAllBytes(Path.Combine("gamedata", $"recv_data_{gamedataId++}.bin"), body);
                        break;
                    case MessageType.GameDataTo:
                        var gamedatato = GameDataTo.Deserialize(reader);
                        HandleGameDataTo(gamedatato, false);
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
                        break;
                    case MessageType.StartGame:
                        var start = StartGame.Deserialize(reader);
                        DumpToConsole(start);
                        break;
                    default:
                        Console.WriteLine($"Unhandled Message: {messageType} size: {body.Length}");
                        return;
                }

                if (reader.GetBytesLeft() > 0 && LogNotConsumed)
                {
                    Console.WriteLine($"{reader.GetBytesLeft()} bytes not cunsumed");
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
                        HandleGameData(gamedata, true);

                        Directory.CreateDirectory("gamedata");
                        File.WriteAllBytes(Path.Combine("gamedata", $"send_data_{gamedataId++}.bin"), body);
                        break;
                    case MessageType.GameDataTo:
                        var gamedatato = GameDataTo.Deserialize(reader);
                        HandleGameDataTo(gamedatato, true);
                        break;
                    case MessageType.GetGameListV2:
                        //var gamelist = GetGameListV2Request.Deserialize(reader);
                        //DumpToConsole(gamelist);
                        break;
                    default:
                        Console.WriteLine($"Unhandled Message: {messageType} size: {body.Length}");
                        return;
                }

                if (reader.GetBytesLeft() > 0 && LogNotConsumed)
                {
                    Console.WriteLine($"{reader.GetBytesLeft()} bytes not cunsumed");
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
                case GameDataType.Move:
                    var move = Move.Deserialize(reader);
                    if (LogMoves)
                    {
                        Console.WriteLine($"[{dir}]Move command player: {move.ownerId:0000} seq: {move.seq:0000} pos: {move.position} delta: {move.velocity}");
                    }

                    //if (reader.HasBytesLeft()) throw new Exception();
                    break;

                case GameDataType.RpcCall:
                    var RPC = RpcCall.Deserialize(reader);
                    DumpToConsole(RPC, LogRPC);
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
                            Console.WriteLine($"[CHAT]{RPC.targetNetId}: {chat.text}");
                            break;
                        case RpcCalls.SendChatNote:
                            var chatnote = RpcSendChatNote.Deserialize(reader);
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
                            //Dont have a message body
                        case RpcCalls.Close:
                        //Currently not implemented
                        case RpcCalls.UpdateGameData:
                        case RpcCalls.SetStartCounter:
                            break;
                        default:
                            Console.WriteLine($"Unhandled RPC command: " + RPC.callId);
                            break;
                    }
                    break;



                case GameDataType.Spawn:
                    Console.WriteLine($"#################################");
                    Console.WriteLine($"Spawn");
                    Console.WriteLine(HexUtils.HexDump(reader.PeekToEnd()));
                    var spawn = Spawn.Deserialize(reader);

                    //Directory.CreateDirectory("spawn");
                    //File.WriteAllBytes(Path.Combine("spawn", $"gamedata_spawn_{spawn_id++}.bin"), data.body);
                    break;
                case GameDataType.SceneChange:
                    var scene = SceneChange.Deserialize(reader);
                    DumpToConsole(scene);
                    break;
                case GameDataType.Despawn:
                    break;
                case GameDataType.Ready:
                    var ready = Ready.Deserialize(reader);
                    Console.WriteLine($"Ready: " + ready.playerId);
                    break;

                default:
                    Console.WriteLine($"Unhandled Gamedatatype: {datatype}");
                    break;
            }

            if (reader.GetBytesLeft() > 0 && LogNotConsumed)
            {
                Console.WriteLine($"{reader.GetBytesLeft()} bytes not cunsumed");
            }
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
