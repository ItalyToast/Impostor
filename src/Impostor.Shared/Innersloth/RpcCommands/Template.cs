//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Impostor.Shared.Innersloth.RpcCommands
//{
//    public class RpcEnterVent
//    {
//        public int id;

//        public static RpcEnterVent Deserialize(HazelBinaryReader reader)
//        {
//            var msg = new RpcEnterVent();

//            msg.id = reader.ReadPackedInt32();

//            return msg;
//        }
//    }
//}
