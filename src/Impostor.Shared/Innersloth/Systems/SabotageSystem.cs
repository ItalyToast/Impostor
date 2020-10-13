using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class SabotageSystem : ISystem
    {
        public float Timer;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            Timer = reader.ReadSingle();
        }
    }
}
