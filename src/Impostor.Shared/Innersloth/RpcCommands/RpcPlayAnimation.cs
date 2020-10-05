using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcPlayAnimation
    {
        public byte animType;

        public static RpcPlayAnimation Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcPlayAnimation();

            msg.animType = reader.ReadByte();

            return msg;
        }
    }
}
