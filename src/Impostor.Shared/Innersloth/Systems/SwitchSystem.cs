using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class SwitchSystem
    {
        public byte ExpectedSwitches;
        public byte ActualSwitches;
        public byte Value;

        public static SwitchSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new SwitchSystem();

            system.ExpectedSwitches = reader.ReadByte();
            system.ActualSwitches = reader.ReadByte();
            system.Value = reader.ReadByte();

            return system;
        }
    }
}
