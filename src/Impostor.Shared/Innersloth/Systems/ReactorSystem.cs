using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class ReactorSystem
    {
        public float Countdown;
        public List<Tuple<byte, byte>> UserConsolePairs;

        public static ReactorSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new ReactorSystem();

            system.Countdown = reader.ReadSingle();
            system.UserConsolePairs.Clear();
            int num = reader.ReadPackedInt32();
            for (int i = 0; i < num; i++)
            {
                system.UserConsolePairs.Add(new Tuple<byte, byte>(reader.ReadByte(), reader.ReadByte()));
            }

            return system;
        }
    }
}
