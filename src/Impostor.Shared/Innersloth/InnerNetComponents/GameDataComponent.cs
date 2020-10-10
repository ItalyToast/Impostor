﻿using System;
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
                Console.WriteLine("Unhandled Gamedata deserialize() on spawn");
                return;
            }
            Console.WriteLine("Unhandled Gamedata deserialize()");
        }
    }
}
