using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcRepairSystem
    {
        public SystemTypes systemType;
        public byte[] netObject;
        public byte amount;

        public static RpcRepairSystem Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcRepairSystem();

            return msg;
        }
    }
}
