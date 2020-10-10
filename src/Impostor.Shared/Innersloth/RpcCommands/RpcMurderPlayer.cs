using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcMurderPlayer
    {
        public int netId;

        public static RpcMurderPlayer Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcMurderPlayer();

            msg.netId = reader.ReadPackedInt32();

            return msg;
        }
    }
}
