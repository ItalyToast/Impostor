using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class CmdCheckColor
    {
        public byte bodyColor;

        public static CmdCheckColor Deserialize(HazelBinaryReader reader)
        {
            var msg = new CmdCheckColor();

            msg.bodyColor = reader.ReadByte();

            return msg;
        }
    }
}
