using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetPet
    {
        public int petId;
        public static RpcSetPet Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetPet();
            msg.petId = reader.ReadPackedInt32();
            return msg;
        }
    }
}
