using System.Collections.Generic;
using System.Runtime.InteropServices;
using Hazel;
using Impostor.Shared.Innersloth;
using System.Linq;

namespace Impostor.Shared.Innersloth.Messages
{
    public class GetGameListV2Response
    {
        public class GameListItem
        {
            public int adress;
            public ushort port;
            public int code;
            public string name;
            public byte players;
            public int age;
            public byte mapid;
            public byte imposters;
            public byte maxplayers;


            public static GameListItem Deserialize(HazelBinaryReader reader)
            {
                var msg = new GameListItem();

                msg.adress = reader.ReadInt32();
                msg.port = reader.ReadUInt16();
                msg.code = reader.ReadInt32();
                msg.name = reader.ReadString();
                msg.players = reader.ReadByte();
                msg.age = reader.ReadPackedInt32();
                msg.mapid = reader.ReadByte();
                msg.imposters = reader.ReadByte();
                msg.maxplayers = reader.ReadByte();
                return msg;
            }
        }

        public List<GameListItem> games;

        public static GetGameListV2Response Deserialize(HazelBinaryReader reader)
        {
            var msg = new GetGameListV2Response();

            var size = reader.ReadInt16();
            var type = reader.ReadByte();
            var body = new HazelBinaryReader(reader.ReadBytes(size));

            msg.games = new List<GameListItem>();

            while (body.HasBytesLeft())
            {
                var itemsize = body.ReadInt16();
                var itemtype = body.ReadByte();
                var itemBody = body.ReadBytes(itemsize);
                var itemreader = new HazelBinaryReader(itemBody);

                msg.games.Add(GameListItem.Deserialize(itemreader));
            }

            return msg;
        }
    }
}