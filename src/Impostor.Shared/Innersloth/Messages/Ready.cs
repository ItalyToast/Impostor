using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class Ready
    {
        public int playerId;

        public static Ready Deserialize(HazelBinaryReader reader)
        {
            var msg = new Ready();
            msg.playerId = reader.ReadPackedInt32();
            return msg;
        }
    }
}
