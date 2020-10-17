using System;

namespace Impostor.Shared.Innersloth.Enums
{
    [Flags]
    public enum MapFlags
    {
        Skeld = 1 << 0,
        MiraHQ = 1 << 1,
        Polus = 1 << 2
    }
}