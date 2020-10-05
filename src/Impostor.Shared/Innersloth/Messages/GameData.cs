using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class GameData
    {
        public int gameId;
        public byte type;
        public byte[] body;

        public static GameData Deserialize(HazelBinaryReader reader)
        {
            var msg = new GameData();

            msg.gameId = reader.ReadInt32();
            var size = reader.ReadInt16();
            msg.type = reader.ReadByte();
            msg.body = reader.ReadBytes(size);

            return msg;
        }
    }
}
