using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class DoorsSystem
    {
		public class Doorway
        {
			public bool isOpen;
        }

        private List<Doorway> doors;

        public static DoorsSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new DoorsSystem();

			if (onSpawn)
			{
				for (int i = 0; i < 10; i++)
				{
					system.doors.Add(new Doorway()
					{
						isOpen = reader.ReadBoolean(),
					});
				}
				return system;
			}
			uint doorFlags = reader.ReadPackedUInt32();
			for (int j = 0; j < system.doors.Count; j++)
			{
				if ((doorFlags & (1U << j)) != 0U)
				{
					system.doors[j].isOpen = reader.ReadBoolean();
				}
			}

			return system;
        }
    }
}
