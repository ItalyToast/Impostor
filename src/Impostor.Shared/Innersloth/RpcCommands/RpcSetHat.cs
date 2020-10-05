using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetHat
    {
        public Hat hatId;
        public static RpcSetHat Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetHat();
            msg.hatId = (Hat)reader.ReadPackedInt32();
            return msg;
        }
    }
}
