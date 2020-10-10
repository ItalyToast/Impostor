using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class PlayerControl : InnerNetObject
    {
        public DummyComponent dummy0;
        public DummyComponent dummy1;
        public CustomNetworkTransform transform;

        public byte playerId;
        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            if (onSpawn)
            {
                Console.WriteLine("PlayerControl ######");
                var b1 = reader.ReadByte();
                playerId = reader.ReadByte();
                Console.WriteLine("byte1: " + b1);
                Console.WriteLine("playerid: " + playerId);
                return;
            }
           
            Console.WriteLine("Unhandled PlayerControl deserialize()");
        }
    }
}
