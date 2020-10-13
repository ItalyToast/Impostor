using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class SecurityCameraSystem : ISystem
    {
        public List<byte> InUseBy;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            InUseBy = reader.ReadList(read => read.ReadByte());
        }
    }
}
