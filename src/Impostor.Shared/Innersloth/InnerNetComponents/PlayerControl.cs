using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class PlayerControl : InnerNetObject
    {
        public CustomNetworkTransform transform;

        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            throw new NotImplementedException();
        }
    }
}
