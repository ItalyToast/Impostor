﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class LobbyBehaviour : InnerNetObject
    {
        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            Console.WriteLine($"Unhandled LobbyBehaviour deserialize() size: {reader.GetBytesLeft()}");
        }
    }
}
