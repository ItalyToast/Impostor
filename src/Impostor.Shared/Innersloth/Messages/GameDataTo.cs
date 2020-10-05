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

        public static GameDataTo Deserialize(HazelBinaryReader reader)
        {
            var msg = new GameDataTo();

            msg.gameId = reader.ReadInt32();
            msg.netId = reader.ReadPackedInt32();
            var size = reader.ReadInt16();
            msg.type = reader.ReadByte();
            msg.body = reader.ReadBytes(size);

            return msg;
        }
    }
}
