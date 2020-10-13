using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class SwitchSystem : ISystem
    {
        public byte ExpectedSwitches;
        public byte ActualSwitches;
        public byte Value;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            ExpectedSwitches = reader.ReadByte();
            ActualSwitches = reader.ReadByte();
            Value = reader.ReadByte();
        }
    }
}
