using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class HudOverrideSystem
    {
        public bool IsActive;

        public static HudOverrideSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new HudOverrideSystem();

            system.IsActive = reader.ReadBoolean();

            return system;
        }
    }
}
