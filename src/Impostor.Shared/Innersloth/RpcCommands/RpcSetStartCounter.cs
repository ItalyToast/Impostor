using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetStartCounter
    {
        public byte b1;
        public byte secondsLeft;

        public static RpcSetStartCounter Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetStartCounter();

            msg.b1 = reader.ReadByte();
            msg.secondsLeft = reader.ReadByte();

            return msg;
        }
    }
}
