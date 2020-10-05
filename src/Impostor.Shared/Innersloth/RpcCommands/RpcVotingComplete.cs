using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcVotingComplete
    {
        public byte[] states;
        public byte exiledPlayerId;
        public bool tie;

        public static RpcVotingComplete Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcVotingComplete();

            msg.states = reader.ReadBytesAndSize();
            msg.exiledPlayerId = reader.ReadByte();
            msg.tie = reader.ReadBoolean();

            return msg;
        }
    }
}
