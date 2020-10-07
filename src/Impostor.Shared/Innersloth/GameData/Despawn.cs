using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.GameData
{
     public class Despawn
    {
        public int netId;

        public static Despawn Deserialize(HazelBinaryReader reader)
        {
            var msg = new Despawn();

            msg.netId = reader.ReadPackedInt32();

            return msg;
        }
    }
}
