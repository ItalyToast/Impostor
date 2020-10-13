using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class MedScanSystem : ISystem
    {
        public List<byte> UsersList;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            UsersList = reader.ReadList(read => read.ReadByte());
        }
    }
}
