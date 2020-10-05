using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSendChat
    {
        public string text;

        public static RpcSendChat Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSendChat();

            msg.text = reader.ReadString();

            return msg;
        }
    }
}
