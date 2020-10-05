using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class JoinGameResponse
    {
        public int gameCode;
        public int playerId;
        public int hostId;

        public static JoinGameResponse Deserialize(HazelBinaryReader reader)
        {
            var msg = new JoinGameResponse();
            msg.gameCode = reader.ReadInt32();
            msg.playerId = reader.ReadInt32();
            msg.hostId = reader.ReadInt32();
            return msg;
        }
    }
}
