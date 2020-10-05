using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.GameData
{
    public class Move
    {
        public int ownerId;
        public int seq;
        public int x;
        public int y;
        public int dx;
        public int dy;

        public static Move Deserialize(HazelBinaryReader reader)
        {
            var msg = new Move();

            msg.ownerId = reader.ReadPackedInt32();
            msg.seq = reader.ReadInt16();
            msg.x = reader.ReadInt16();
            msg.y = reader.ReadInt16();
            msg.dx = reader.ReadInt16();
            msg.dy = reader.ReadInt16();

            //msg.x = msg.x ^ 0x7FFF;
            //msg.y = msg.y ^ 0x7FFF;

            return msg;
        }
    }
}
