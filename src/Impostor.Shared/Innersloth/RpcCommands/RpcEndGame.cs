using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcEndGame
    {
        public GameOverReason endReason;
        public bool showAd;

        public static RpcEndGame Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcEndGame();

            msg.endReason = (GameOverReason)reader.ReadByte();
            msg.showAd = reader.ReadBoolean();

            return msg;
        }
    }
}
