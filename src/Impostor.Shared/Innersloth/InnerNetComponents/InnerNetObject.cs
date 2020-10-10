using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth
{
    public abstract class InnerNetObject
    {
		public uint SpawnId;

		public int NetId;

		public uint DirtyBits;

		public SpawnFlags SpawnFlags;

		// Token: 0x040007C9 RID: 1993
		public int OwnerId;

		// Token: 0x040007CA RID: 1994
		protected bool DespawnOnDestroy = true;

		public abstract void Deserialize(HazelBinaryReader reader, bool onSpawn);

		public virtual void HandleRpcCall(RpcCalls rpcCall, HazelBinaryReader reader)
        {
			Console.WriteLine($"[{GetType().Name}]Recived RPC call: {rpcCall}");
        }
    }
}
