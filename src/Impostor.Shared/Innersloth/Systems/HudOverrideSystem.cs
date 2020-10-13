using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class HudOverrideSystem : ISystem
    {
        public bool IsActive;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            IsActive = reader.ReadBoolean();
        }
    }
}
