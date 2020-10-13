using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public interface ISystem
    {
        void Deserialize(HazelBinaryReader reader, bool onSpawn);
    }
}
