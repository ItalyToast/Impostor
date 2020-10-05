using Impostor.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Impostor.Shared.Innersloth.GameData
{
    public class Move
    {
        static FloatRange xyrange = new FloatRange(-40, 40);

        public int ownerId;
        public int seq;
        public Vector2 position;
        public Vector2 velocity;

        public static Move Deserialize(HazelBinaryReader reader)
        {
            var msg = new Move();

            msg.ownerId = reader.ReadPackedInt32();
            msg.seq = reader.ReadInt16();
            msg.position = reader.ReadLerpVector2(xyrange, xyrange);
            msg.velocity = reader.ReadLerpVector2(xyrange, xyrange);

            return msg;
        }
    }
}
