using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetSkin
    {
        public Skin skinId;
        public static RpcSetSkin Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetSkin();
            msg.skinId = (Skin)reader.ReadPackedInt32();
            return msg;
        }
    }
}
