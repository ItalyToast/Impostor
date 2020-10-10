using Impostor.Shared.Innersloth.Enums;
using Impostor.Shared.Innersloth.InnerNetComponents;
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

		public int OwnerId;

		protected bool DespawnOnDestroy = true;

		public abstract void Deserialize(HazelBinaryReader reader, bool onSpawn);

		public virtual void HandleRpcCall(RpcCalls rpcCall, HazelBinaryReader reader)
        {
            if (this is DummyComponent dummy)
            {
                Console.WriteLine($"[{GetType().Name}][{dummy.name}]Recived RPC call: {rpcCall}");
            }
            else
            {
                Console.WriteLine($"[{GetType().Name}]Recived RPC call: {rpcCall}");
            }
        }
    }
}
