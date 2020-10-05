using Impostor.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSnapTo
    {
        static FloatRange xyrange = new FloatRange(-40, 40);

        public Vector2 position;
        public ushort lastSequenceId;

        public static RpcSnapTo Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSnapTo();

            msg.position = reader.ReadLerpVector2(xyrange, xyrange);
            msg.lastSequenceId = reader.ReadUInt16();

            return msg;
        }
    }
}
