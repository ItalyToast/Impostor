using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class HostGameRequest
    {
        public GameOptionsData gameOptions;

        public static HostGameRequest Deserialize(HazelBinaryReader reader)
        {
            var result = new HostGameRequest();
            result.gameOptions = GameOptionsData.Deserialize(reader.ReadBytesAndSize());
            return result;
        }
    }
}
