using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class DeconSystem : ISystem
    {
        public float timer;
        public States curState;

        public void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            timer = (float)reader.ReadByte();
            curState = (States)reader.ReadByte();
        }

        public enum States
        {
            Idle = 0,
            Enter = 1,
            Closed = 2,
            Exit = 4,
            HeadingUp = 8
        }
    }
}
