using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcUpdateGameData
    {
        public string name;
        public static RpcUpdateGameData Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcUpdateGameData();

            //var u1 = reader.ReadBytes(3);
            //msg.name = reader.ReadString();
            //var u2 = reader.ReadBytes(6);
            return msg;
        }
    }
}
