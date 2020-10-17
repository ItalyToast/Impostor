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
        public byte[] data;

        public static Data Deserialize(HazelBinaryReader reader)
        {
            var msg = new Data();

            msg.netId = reader.ReadPackedInt32();
            msg.data = reader.ReadBytesToEnd();
            
            return msg;
        }
    }
}
