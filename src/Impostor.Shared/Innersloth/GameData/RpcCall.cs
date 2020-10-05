using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.GameData
{
    public class RpcCall
    {
        public int targetNetId;
        public RpcCalls callId;

        public static RpcCall Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcCall();

            msg.targetNetId = reader.ReadPackedInt32();
            msg.callId = (RpcCalls)reader.ReadByte();

            return msg;
        }
    }
}
