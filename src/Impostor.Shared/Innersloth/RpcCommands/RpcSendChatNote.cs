using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSendChatNote
    {
        public static RpcSendChatNote Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSendChatNote();

            return msg;
        }
    }
}
