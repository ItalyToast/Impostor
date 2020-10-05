using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class SendChat
    {
        public string text;

        public static SendChat Deserialize(HazelBinaryReader reader)
        {
            var msg = new SendChat();

            msg.text = reader.ReadString();

            return msg;
        }
    }
}
