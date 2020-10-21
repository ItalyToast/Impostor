using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class DoorsSystem : ISystem
	{
		public class Doorway
        {
			public bool isOpen;
        }

        private List<Doorway> doors = new List<Doorway>();

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
			if (onSpawn)
			{
				//This number of doors depends on the map
				//Skeld: 13
				//HQ: 2?
				//Polus: 12
				for (int i = 0; i < 12; i++)
				{
					doors.Add(new Doorway()
					{
						isOpen = reader.ReadBoolean(),
					});
				}
				return;
			}
			uint doorFlags = reader.ReadPackedUInt32();
			for (int j = 0; j < doors.Count; j++)
			{
				if ((doorFlags & (1U << j)) != 0U)
				{
					doors[j].isOpen = reader.ReadBoolean();
				}
			}
        }
    }
}
