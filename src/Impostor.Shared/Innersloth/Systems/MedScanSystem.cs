using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class MedScanSystem
    {
        public List<byte> UsersList;

        public static MedScanSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new MedScanSystem();

            system.UsersList.Clear();
            int num = reader.ReadPackedInt32();
            for (int i = 0; i < num; i++)
            {
                system.UsersList.Add(reader.ReadByte());
            }

            return system;
        }
    }
}
