using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetScanner
    {
        public bool value;
        public byte b2;

        public static RpcSetScanner Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetScanner();

            msg.value = reader.ReadBoolean();
            msg.b2 = reader.ReadByte();

            return msg;
        }
    }
}
