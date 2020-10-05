using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class JoinGameRequest
    {
        public int gameCode;
        public byte unknown;

        public static JoinGameRequest Deserialize(HazelBinaryReader reader)
        {
            var msg = new JoinGameRequest();
            msg.gameCode = reader.ReadInt32();
            msg.unknown = reader.ReadByte();
            return msg;
        }
    }
}
