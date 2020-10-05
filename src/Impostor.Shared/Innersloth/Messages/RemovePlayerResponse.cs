using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class RemovePlayerResponse
    {
        public int gameCode;
        public int playerId;
        public int hostId;
        public DisconnectReason reason;
        public static RemovePlayerResponse Deserialize(HazelBinaryReader reader)
        {
            var msg = new RemovePlayerResponse();
            msg.gameCode = reader.ReadInt32();
            msg.playerId = reader.ReadPackedInt32();
            msg.hostId = reader.ReadInt32();
            msg.reason = (DisconnectReason)reader.ReadByte();
            return msg;
        }
    }
}
