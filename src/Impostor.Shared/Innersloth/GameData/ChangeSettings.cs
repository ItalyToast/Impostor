using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.GameData
{
    public class ChangeSettings
    {
        public static ChangeSettings Deserialize(HazelBinaryReader reader)
        {
            var msg = new ChangeSettings();

            return msg;
        }
    }
}
