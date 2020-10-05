using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetName
    {
        public string name;

        public static RpcSetName Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetName();

            msg.name = reader.ReadString();

            return msg;
        }
    }
}
