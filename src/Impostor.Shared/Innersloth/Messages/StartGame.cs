using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class StartGame
    {
        public int unknownid;

        public static StartGame Deserialize(HazelBinaryReader reader)
        {
            var msg = new StartGame();
            msg.unknownid = reader.ReadInt32();
            return msg;
        }
    }
}
