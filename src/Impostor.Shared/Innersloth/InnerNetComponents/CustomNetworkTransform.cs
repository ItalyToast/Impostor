using Impostor.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class CustomNetworkTransform : InnerNetObject
    {
		static FloatRange xyrange = new FloatRange(-40, 40);

        public short seq;
		public Vector2 position;
		public Vector2 velocity;

        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
			if (onSpawn)
			{
				seq = reader.ReadInt16();
				position = reader.ReadLerpVector2(xyrange, xyrange);
				velocity = reader.ReadLerpVector2(xyrange, xyrange);
				return;
			}

			seq = reader.ReadInt16();
			position = reader.ReadLerpVector2(xyrange, xyrange);
			velocity = reader.ReadLerpVector2(xyrange, xyrange);

			//ushort newSid = reader.ReadUInt16();
			//if (!CustomNetworkTransform.SidGreaterThan(newSid, this.lastSequenceId))
			//{
			//	return;
			//}
			//this.lastSequenceId = newSid;
			//this.targetSyncPosition = this.ReadVector2(reader);
			//this.targetSyncVelocity = this.ReadVector2(reader);
			//if (!base.isActiveAndEnabled)
			//{
			//	return;
			//}
			//if (Vector2.Distance(this.body.position, this.targetSyncPosition) > this.snapThreshold)
			//{
			//	if (this.body)
			//	{
			//		this.body.position = this.targetSyncPosition;
			//		this.body.velocity = this.targetSyncVelocity;
			//	}
			//	else
			//	{
			//		base.transform.position = this.targetSyncPosition;
			//	}
			//}
			//if (this.interpolateMovement == 0f && this.body)
			//{
			//	this.body.position = this.targetSyncPosition;
			//}
		}
    }
}
