using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class KickPlayerResponse
    {
        public int gameCode;
        public int playerId;
        public bool isBan;
        public static KickPlayerResponse Deserialize(HazelBinaryReader reader)
        {
            var msg = new KickPlayerResponse();
            msg.gameCode = reader.ReadInt32();
            msg.playerId = reader.ReadPackedInt32();
            msg.isBan = reader.ReadBoolean();
            return msg;
        } 
    }
}
