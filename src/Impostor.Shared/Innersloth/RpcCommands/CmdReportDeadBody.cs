using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class CmdReportDeadBody
    {
        public byte playerId;

        public static CmdReportDeadBody Deserialize(HazelBinaryReader reader)
        {
            var msg = new CmdReportDeadBody();

            msg.playerId = reader.ReadByte();

            return msg;
        }
    }
}
