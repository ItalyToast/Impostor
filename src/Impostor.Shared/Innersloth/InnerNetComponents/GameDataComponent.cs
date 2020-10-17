using Impostor.Shared.Innersloth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public enum PlayerInfoFlags
    {
        Disconnected = 1 << 0,
        IsImpostor = 1 << 1,
        IsDead = 1 << 2,
    }

    public class TaskInfo
    {
        public uint Id;
        public bool Complete;

        public void Deserialize(HazelBinaryReader reader)
        {
            Id = reader.ReadPackedUInt32();
            Complete = reader.ReadBoolean();
        }
    }

    public class PlayerInfo
    {
        public byte PlayerId;
        public string PlayerName;
        public byte ColorId;
        public Hat HatId;
        public Skin SkinId;
        public PlayerInfoFlags Flags;
        public List<TaskInfo> Tasks;
        public byte unknown;

        public PlayerInfo(byte playerId)
        {
            PlayerId = playerId;
        }

        public void Deserialize(HazelBinaryReader reader)
        {
            PlayerName = reader.ReadString();
            ColorId = reader.ReadByte();
            HatId = (Hat)reader.ReadPackedUInt32();
            SkinId = (Skin)reader.ReadPackedUInt32();
            Flags = (PlayerInfoFlags)reader.ReadByte();
            Tasks = reader.ReadList(read => {
                var task = new TaskInfo();
                task.Deserialize(read);
                return task;
            });
            unknown = reader.ReadByte();
        }
    }

    public class GameDataComponent : InnerNetObject
    {
        public List<PlayerInfo> AllPlayers;

        public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
            if (onSpawn)
            {
                //Guid gameGuid = new Guid(reader.ReadBytesAndSize());
                AllPlayers = reader.ReadList(read =>
                {
                    var playerInfo = new PlayerInfo(reader.ReadByte());
                    playerInfo.Deserialize(reader);
                    return playerInfo;
                });
                if (reader.HasBytesLeft())
                {
                    Console.WriteLine($"Unhandled Gamedata deserialize() on spawn size: {reader.GetBytesLeft()}");
                }
                return;
            }
            else
            {
                byte count = reader.ReadByte();
                for (int j = 0; j < count; j++)
                {
                    byte playerId = reader.ReadByte();
                    PlayerInfo playerInfo2 = AllPlayers.FirstOrDefault(p => p.PlayerId == playerId);
                    if (playerInfo2 != null)
                    {
                        playerInfo2.Deserialize(reader);
                    }
                    else
                    {
                        playerInfo2 = new PlayerInfo(playerId);
                        playerInfo2.Deserialize(reader);
                        AllPlayers.Add(playerInfo2);
                    }
                }
                if (reader.HasBytesLeft())
                {
                    Console.WriteLine($"Unhandled Gamedata deserialize() size: {reader.GetBytesLeft()}");
                }
            }
        }
    }
}
