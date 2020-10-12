using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Systems
{
    public class DeconSystem
    {
        public float timer;
        public States curState;

        public static DeconSystem Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            var system = new DeconSystem();

            system.timer = (float)reader.ReadByte();
            system.curState = (States)reader.ReadByte();

            return system;
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
