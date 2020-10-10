using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class EndGame
    {
        public int gameId;

        public static EndGame Deserialize(HazelBinaryReader reader)
        {
            var msg = new EndGame();

            var b1 = reader.ReadByte();
            var b2 = reader.ReadByte();
            msg.gameId = reader.ReadInt32();

            return msg;
        }
    }
}
