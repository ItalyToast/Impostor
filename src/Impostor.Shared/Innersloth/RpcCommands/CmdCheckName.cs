using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class CmdCheckName
    {
        public string name;

        public static CmdCheckName Deserialize(HazelBinaryReader reader)
        {
            var msg = new CmdCheckName();

            msg.name = reader.ReadString();

            return msg;
        }
    }
}
