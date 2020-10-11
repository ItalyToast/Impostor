using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class GameDataComponent : InnerNetObject
    {
        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            if (onSpawn)
            {
                Console.WriteLine($"Unhandled Gamedata deserialize() on spawn size: {reader.GetBytesLeft()}");
                return;
            }
            Console.WriteLine($"Unhandled Gamedata deserialize() size: {reader.GetBytesLeft()}");
        }
    }
}
