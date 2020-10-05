using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class WaitForHost
    {
        public int gameCode;
        public int playerId;

        public static WaitForHost Deserialize(HazelBinaryReader reader)
        {
            var msg = new WaitForHost();
            msg.gameCode = reader.ReadInt32();
            msg.playerId = reader.ReadPackedInt32();
            return msg;
        }
    }
}
