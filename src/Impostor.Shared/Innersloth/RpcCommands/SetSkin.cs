using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class SetSkin
    {
        public Skin skinId;
        public static SetSkin Deserialize(HazelBinaryReader reader)
        {
            var msg = new SetSkin();
            msg.skinId = (Skin)reader.ReadByte();
            return msg;
        }
    }
}
