using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class SecurityCameraSystem
    {
        public byte InUse;

        public static SecurityCameraSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new SecurityCameraSystem();

            system.InUse = reader.ReadByte();

            return system;
        }
    }
}
