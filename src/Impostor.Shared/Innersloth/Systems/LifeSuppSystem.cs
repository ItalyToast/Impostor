using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class LifeSuppSystem : ISystem
    {
        public float Countdown;
        public List<int> CompletedConsoles;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            Countdown = reader.ReadSingle();
            if (reader.HasBytesLeft())
            {
                CompletedConsoles = reader.ReadList(read => reader.ReadPackedInt32());
            }
        }
    }
}
