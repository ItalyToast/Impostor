using Impostor.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Impostor.Shared.Innersloth.GameData
{
    public class Data
    {
        static FloatRange xyrange = new FloatRange(-40, 40);

        public int netId;
        public int seq;
        public byte[] data;
        public Vector2 position;
        public Vector2 velocity;

        public static Data Deserialize(HazelBinaryReader reader)
        {
            var msg = new Data();

            msg.netId = reader.ReadPackedInt32();
            msg.data = reader.ReadBytesToEnd();
            //msg.seq = reader.ReadInt16();
            //msg.data = reader.ReadBytesToEnd();
            //msg.position = reader.ReadLerpVector2(xyrange, xyrange);

            ////if the player is not dead - we read the velocity vector aswell
            ////in the real game this is known by the reciver so they dont need to do this check
            //if (reader.HasBytesLeft())
            //{
            //    msg.velocity = reader.ReadLerpVector2(xyrange, xyrange);
            //}
            
            return msg;
        }
    }
}
