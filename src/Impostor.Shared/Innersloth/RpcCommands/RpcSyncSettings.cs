using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSyncSettings
    {
        public GameOptionsData gameOptions;

        public static RpcSyncSettings Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSyncSettings();

            var body = reader.ReadBytesToEnd();
            msg.gameOptions = GameOptionsData.Deserialize(body);

            return msg;
        }
    }
}
