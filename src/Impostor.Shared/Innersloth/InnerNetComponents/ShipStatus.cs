using Impostor.Shared.Innersloth.Enums;
using Impostor.Shared.Innersloth.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class ShipStatus : InnerNetObject
    {
        Dictionary<SystemTypes, ISystem> systems = new Dictionary<SystemTypes, ISystem>()
        {
            {
                SystemTypes.Electrical,
                new SwitchSystem()
            },
            {
                SystemTypes.MedBay,
                new MedScanSystem()
            },
            {
                SystemTypes.Reactor,
                new ReactorSystem()
            },
            {
                SystemTypes.LifeSupp,
                new LifeSuppSystem()
            },
            {
                SystemTypes.Security,
                new SecurityCameraSystem()
            },
            {
                SystemTypes.Comms,
                new HudOverrideSystem()
            },
            {
                SystemTypes.Doors,
                new DoorsSystem()
            },
            {
                SystemTypes.Sabotage,
                new SabotageSystem()
                //(IActivatable) this.Systems[SystemTypes.Comms],
                //(IActivatable)this.Systems[SystemTypes.Reactor],
                //(IActivatable)this.Systems[SystemTypes.LifeSupp],
                //(IActivatable)this.Systems[SystemTypes.Electrical]
            }
            };

        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            if (onSpawn)
            {
                for (int i = 0; i < 24; i++)
                {
                    var system = (SystemTypes)i;
                    if (systems.ContainsKey(system))
                    {
                        var systemObj = systems[system];
                        systemObj.Deserialize(reader, onSpawn);
                    }
                }
                if (reader.HasBytesLeft())
                {
                    Console.WriteLine($"Bytesleft in ShipStatus DeserializeOnSpawn: size: {reader.GetBytesLeft()}");
                }
                return;
            }

            uint systemFlags = reader.ReadPackedUInt32();
            for (int i = 0; i < 24; i++)
            {
                if ((systemFlags & (1 << i)) != 0)
                {
                    var system = (SystemTypes)i;
                    var systemObj = systems[system];
                    systemObj.Deserialize(reader, onSpawn);
                }
            }
            if (reader.HasBytesLeft())
            {
                Console.WriteLine($"Bytesleft in ShipStatus Deserialize: size: {reader.GetBytesLeft()}");
            }
        }
    }
}
