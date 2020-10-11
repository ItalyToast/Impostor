using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcAddVote
    {
        public int playerId;//not certain about these name
        public int suspectIdx;//not certain about these name

        public static RpcAddVote Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcAddVote();

            msg.playerId = reader.ReadInt32();
            msg.suspectIdx = reader.ReadInt32();

            return msg;
        }
    }
}
