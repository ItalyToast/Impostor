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

            msg.gameId = reader.ReadInt32();

            return msg;
        }
    }
}
