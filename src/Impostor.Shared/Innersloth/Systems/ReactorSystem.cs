using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class ReactorSystem : ISystem
    {
        public float Countdown;
        public List<Tuple<byte, byte>> UserConsolePairs = new List<Tuple<byte, byte>>();

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            Countdown = reader.ReadSingle();
            UserConsolePairs = reader.ReadList(read => new Tuple<byte, byte>(reader.ReadByte(), reader.ReadByte()));
        }
    }
}
