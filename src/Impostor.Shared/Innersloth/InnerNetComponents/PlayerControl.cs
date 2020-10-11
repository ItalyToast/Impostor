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
                bool isNew = reader.ReadBoolean();
                playerId = reader.ReadByte();
                Console.WriteLine("isNew: " + isNew);
                Console.WriteLine("playerid: " + playerId);
                return;
            }
            playerId = reader.ReadByte();
            Console.WriteLine("playerid: " + playerId);
        }
    }
}
