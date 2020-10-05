using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class AlterGameResponse
    {
        public int gameCode;
        public AlterGameTags gameTag;
        public bool value;

        public static AlterGameResponse Deserialize(HazelBinaryReader reader)
        {
            var msg = new AlterGameResponse();
            msg.gameCode = reader.ReadInt32();
            msg.gameTag = (AlterGameTags)reader.ReadByte();
            msg.value = reader.ReadBoolean();
            return msg;
        }
    }
}
