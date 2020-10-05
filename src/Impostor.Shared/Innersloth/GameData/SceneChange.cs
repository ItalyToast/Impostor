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

        public static SceneChange Deserialize(HazelBinaryReader reader)
        {
            var msg = new SceneChange();

            msg.sceneName = reader.ReadString();

            return msg;
        }
    }
}
