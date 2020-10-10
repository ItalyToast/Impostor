using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class GameDataTo
    {
        public int gameId;
        public int netId;
        public byte type;
        public byte[] body;

        public static List<GameDataTo> Deserialize(HazelBinaryReader reader)
        {
            var gameId = reader.ReadInt32();
            var netId = reader.ReadPackedInt32();
            var gamedatas = new List<GameDataTo>();

            while (reader.HasBytesLeft())
            {
                var msg = new GameDataTo();
                msg.gameId = gameId;
                msg.netId = netId;
                var size = reader.ReadInt16();
                msg.type = reader.ReadByte();
                msg.body = reader.ReadBytes(size);
                gamedatas.Add(msg);
            }

            return gamedatas;
        }
    }
}
