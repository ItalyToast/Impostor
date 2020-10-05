using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcCompleteTask
    {
        public int idx;

        public static RpcCompleteTask Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcCompleteTask();

            msg.idx = reader.ReadPackedInt32();

            return msg;
        }
    }
}
