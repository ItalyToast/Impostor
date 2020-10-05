using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class JoinedGame
    {
        public int gameCode;
        public int playerId;
        public int hostId;
        public List<int> otherPlayerIds;

        public static JoinedGame Deserialize(HazelBinaryReader reader)
        {
            var msg = new JoinedGame();
            msg.gameCode = reader.ReadInt32();
            msg.playerId = reader.ReadInt32();
            msg.hostId = reader.ReadInt32();
            msg.otherPlayerIds = reader.ReadList(read => read.ReadPackedInt32());
            return msg;
        }
    }
}
