using Impostor.Shared.Innersloth.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class DummyComponent : InnerNetObject
    {
        public string name;

        public static DummyComponent Spawn(int ownerId, Spawn.SpawnChild spawn)
        {
            return new DummyComponent()
            {
                OwnerId = ownerId,
                NetId = spawn.netId,
            };
        }

        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            Console.WriteLine($"Attempted Deserialize for DummyComponent: {name} size: {reader.GetBytesLeft()}");
        }
    }
}
