using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcStartMeeting
    {
        public byte playerId;

        public static RpcStartMeeting Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcStartMeeting();

            msg.playerId = reader.ReadByte();

            return msg;
        }
    }
}
