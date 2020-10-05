using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class RemovePlayerRequest
    {
        public int playerId;
        public bool isBan;
        public static RemovePlayerRequest Deserialize(HazelBinaryReader reader)
        {
            var msg = new RemovePlayerRequest();
            msg.playerId = reader.ReadPackedInt32();
            msg.isBan = reader.ReadBoolean();
            return msg;
        }
    }
}
