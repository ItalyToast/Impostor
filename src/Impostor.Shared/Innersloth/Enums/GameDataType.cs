using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Enums
{
    public enum GameDataType
    {
        Move = 1,
        RpcCall = 2,
        Spawn = 4,
        Despawn = 5,
        SceneChange = 6,
        Ready = 7,
        ChangeSettings = 8,
    }
}
