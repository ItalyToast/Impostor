using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.GameData
{
    public class Spawn
    {
        public int spawnId;
		public int ownerId;
		public SpawnFlags flags;
		public List<SpawnChild> children;

        public class SpawnChild
        {
			public int netId;
			public byte[] body;
        }

        public static Spawn Deserialize(HazelBinaryReader reader)
        {
            var msg = new Spawn();

			msg.spawnId = reader.ReadPackedInt32();
			msg.ownerId = reader.ReadPackedInt32();
			msg.flags = (SpawnFlags)reader.ReadByte();

			msg.children = new List<SpawnChild>();
			var count = reader.ReadPackedInt32();
            for (int i = 0; i < count; i++)
            {
				var netId = reader.ReadPackedInt32();
				var size = reader.ReadInt16();
				var type = reader.ReadByte();
				var body = reader.ReadBytes(size);

				msg.children.Add(new SpawnChild()
				{
					netId = netId,
					body = body,
				});
			}

			return msg;
        }
    }
}
