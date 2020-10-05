using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class AlterGameRequest 
    {
        public AlterGameTags gameTag;
        public bool value;

        public static AlterGameRequest Deserialize(HazelBinaryReader reader)
        {
            var msg = new AlterGameRequest();
            msg.gameTag = (AlterGameTags)reader.ReadByte();
            msg.value = reader.ReadBoolean();
            return msg;
        }
    }
}
