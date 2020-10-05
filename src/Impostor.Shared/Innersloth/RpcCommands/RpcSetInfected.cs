using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetInfected
    {
        public List<byte> infectedIds;

        public static RpcSetInfected Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetInfected();

            msg.infectedIds = reader.ReadList(read => read.ReadByte());

            return msg;
        }
    }
}
