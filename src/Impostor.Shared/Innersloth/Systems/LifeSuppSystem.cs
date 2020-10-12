using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class LifeSuppSystem
    {
        public float Countdown;
        public List<int> CompletedConsoles;

        public static LifeSuppSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new LifeSuppSystem();

            system.Countdown = reader.ReadSingle();
            if (reader.HasBytesLeft())
            {
                system.CompletedConsoles.Clear();
                int num = reader.ReadPackedInt32();
                for (int i = 0; i < num; i++)
                {
                    system.CompletedConsoles.Add(reader.ReadPackedInt32());
                }
            }

            return system;
        }
    }
}
