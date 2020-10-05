using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.GameData
{
    public class SceneChange
    {
        public string sceneName;
        public int clientId;

        public static SceneChange Deserialize(HazelBinaryReader reader)
        {
            var msg = new SceneChange();

            msg.clientId = reader.ReadPackedInt32();
            msg.sceneName = reader.ReadString();

            return msg;
        }
    }
}
