using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetColor
    {
        public byte color;

        public static RpcSetColor Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetColor();

            msg.color = reader.ReadByte();

            return msg;
        }
    }
}
