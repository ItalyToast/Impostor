using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcCloseDoorsOfType
    {
        public SystemTypes type;

        public static RpcCloseDoorsOfType Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcCloseDoorsOfType();

            msg.type = (SystemTypes)reader.ReadByte();

            return msg;
        }
    }
}
