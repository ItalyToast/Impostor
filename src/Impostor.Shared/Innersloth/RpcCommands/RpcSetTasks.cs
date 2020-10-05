using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSetTasks
    {
        public int playerId;
        public byte[] taskTypeIds;

        public static RpcSetTasks Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSetTasks();

            msg.playerId = reader.ReadByte();
            msg.taskTypeIds = reader.ReadBytesAndSize();

            return msg;
        }
    }
}
