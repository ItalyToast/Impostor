using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class SabotageSystem
    {
        public float Timer;

        public static SabotageSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new SabotageSystem();

            system.Timer = reader.ReadSingle();

            return system;
        }
    }
}
