using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class ShipStatus : InnerNetObject
    {
        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            if (onSpawn)
            {
                for (int i = 0; i < 24; i++)
                {
                    var system = (SystemTypes)i;
                    DesializeSystem(system, reader);
                }
                return;
            }

            uint systemFlags = reader.ReadPackedUInt32();
            for (int i = 0; i < 24; i++)
            {
                if ((systemFlags & (1 << i)) != 0)
                {
                    var system = (SystemTypes)i;
                    DesializeSystem(system, reader);
                }
            }
        }

        public void DesializeSystem(SystemTypes system, HazelBinaryReader reader)
        {
            switch (system)
            {
                case SystemTypes.Hallway:
                    break;
                case SystemTypes.Storage:
                    break;
                case SystemTypes.Cafeteria:
                    break;
                case SystemTypes.Reactor:
                    break;
                case SystemTypes.UpperEngine:
                    break;
                case SystemTypes.Nav:
                    break;
                case SystemTypes.Admin:
                    break;
                case SystemTypes.Electrical:
                    break;
                case SystemTypes.LifeSupp:
                    break;
                case SystemTypes.Shields:
                    break;
                case SystemTypes.MedBay:
                    break;
                case SystemTypes.Security:
                    break;
                case SystemTypes.Weapons:
                    break;
                case SystemTypes.LowerEngine:
                    break;
                case SystemTypes.Comms:
                    break;
                case SystemTypes.ShipTasks:
                    break;
                case SystemTypes.Doors:
                    break;
                case SystemTypes.Sabotage:
                    break;
                case SystemTypes.Decontamination:
                    break;
                case SystemTypes.Launchpad:
                    break;
                case SystemTypes.LockerRoom:
                    break;
                case SystemTypes.Laboratory:
                    break;
                case SystemTypes.Balcony:
                    break;
                case SystemTypes.Office:
                    break;
                case SystemTypes.Greenhouse:
                    break;
                default:
                    throw new Exception("Unhandeled System: " + system);
            }
        }
    }
}
