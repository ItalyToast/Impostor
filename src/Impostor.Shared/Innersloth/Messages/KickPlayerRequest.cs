using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class KickPlayerRequest
    {
        public int playerId;
        public bool isBan;
        public static KickPlayerRequest Deserialize(HazelBinaryReader reader)
        {
            var msg = new KickPlayerRequest();
            msg.playerId = reader.ReadPackedInt32();
            msg.isBan = reader.ReadBoolean();
            return msg;
        } 
    }
}
