using System.Collections.Generic;
using Hazel;
using Impostor.Shared.Innersloth;

namespace Impostor.Shared.Innersloth.Messages
{
    public class GetGameListV2Request
    {
        public GameOptionsData options;

        public static GetGameListV2Request Deserialize(HazelBinaryReader reader)
        {
            var msg = new GetGameListV2Request();
            reader.ReadPackedInt32(); // Hardcoded 0.
            msg.options = GameOptionsData.Deserialize(reader.ReadBytesAndSize());
            return msg;
        }
    }
}