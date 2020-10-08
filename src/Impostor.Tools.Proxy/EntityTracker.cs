using Impostor.Shared.Innersloth;
using Impostor.Shared.Innersloth.InnerNetComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Tools.Proxy
{
    class EntityTracker
    {
        public static Dictionary<int, InnerNetObject> entities = new Dictionary<int, InnerNetObject>();

        public static void Add(InnerNetObject netObj)
        {
            entities.Add(netObj.NetId, netObj);
        }
    }
}
