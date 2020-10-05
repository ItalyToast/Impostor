using Impostor.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class HostGameResponse
    {
        public int gameCode;

        public static HostGameResponse Deserialize(HazelBinaryReader reader)
        {
            var msg = new HostGameResponse();
            msg.gameCode = reader.ReadInt32();
            return msg;
        }
    }
}
