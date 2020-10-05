using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class SetHat
    {
        public Hat hatId;
        public static SetHat Deserialize(HazelBinaryReader reader)
        {
            var msg = new SetHat();
            msg.hatId = (Hat)reader.ReadByte();
            return msg;
        }
    }
}
