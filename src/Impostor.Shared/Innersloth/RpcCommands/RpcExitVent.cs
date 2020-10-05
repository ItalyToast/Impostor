using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcExitVent
    {
        public int id;

        public static RpcExitVent Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcExitVent();

            msg.id = reader.ReadPackedInt32();

            return msg;
        }
    }
}
