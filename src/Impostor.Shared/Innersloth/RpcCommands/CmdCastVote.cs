using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class CmdCastVote
    {
        public byte playerId;
        public sbyte suspectIdx;

        public static CmdCastVote Deserialize(HazelBinaryReader reader)
        {
            var msg = new CmdCastVote();

            msg.playerId = reader.ReadByte();
            msg.suspectIdx = reader.ReadSByte();

            return msg;
        }
    }
}
