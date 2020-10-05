using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class SendChatNote
    {
        public static SendChatNote Deserialize(HazelBinaryReader reader)
        {
            var msg = new SendChatNote();

            return msg;
        }
    }
}
