using System.Collections.Generic;
using System.Runtime.InteropServices;
using Hazel;
using Impostor.Shared.Innersloth;

namespace Impostor.Shared.Innersloth.Messages
{
    public class GetGameListV2Response
    {
        public class GameListItem
        {
            //public static GameInfo Deserialize(HazelBinaryReader reader)
            //{
            //    writer.Write(game.PublicIp.Address.GetAddressBytes());
            //    writer.Write((ushort)game.PublicIp.Port);
            //    writer.Write(game.Code);
            //    writer.Write(game.Host.Client.Name);
            //    writer.Write((byte)game.PlayerCount);
            //    writer.WritePacked(1); // TODO: What does Age do?
            //    writer.Write((byte)game.Options.MapId);
            //    writer.Write((byte)game.Options.NumImpostors);
            //    writer.Write((byte)game.Options.MaxPlayers);
            //}
        }

        public int skeld;
        public int miraHQ;
        public int polus;
        //public List<GameInfo> games;

        public static GetGameListV2Response Deserialize(HazelBinaryReader reader)
        {
            var msg = new GetGameListV2Response();
            //int headertype;
            //var header = reader.ReadMessage(out headertype);

            //msg.skeld = reader.ReadInt32();
            //msg.miraHQ = reader.ReadInt32();
            //msg.polus = reader.ReadInt32();



            //msg.games = reader.ReadList(read => GameInfo.Deserialize(read));
            return msg;
        }

        //public static void Serialize(MessageWriter writer, IEnumerable<Game> games)
        //{
        //    //writer.StartMessage(MessageFlags.GetGameListV2);
                
        //    // Count
        //    writer.StartMessage(1);
        //    writer.Write(123); // The Skeld
        //    writer.Write(456); // Mira HQ
        //    writer.Write(789); // Polus
        //    writer.EndMessage();
                
        //    // Listing
        //    writer.StartMessage(0);
        //    foreach (var game in games)
        //    {
        //        writer.StartMessage(0);
        //        writer.Write(game.PublicIp.Address.GetAddressBytes());
        //        writer.Write((ushort) game.PublicIp.Port);
        //        writer.Write(game.Code);
        //        writer.Write(game.Host.Client.Name);
        //        writer.Write((byte) game.PlayerCount);
        //        writer.WritePacked(1); // TODO: What does Age do?
        //        writer.Write((byte) game.Options.MapId);
        //        writer.Write((byte) game.Options.NumImpostors);
        //        writer.Write((byte) game.Options.MaxPlayers);
        //        writer.EndMessage();
        //    }
        //    writer.EndMessage();
                
        //    writer.EndMessage();
        //}
    }
}