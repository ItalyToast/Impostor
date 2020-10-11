using Impostor.Shared.Innersloth.Enums;
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
        public byte playerId;
        public Hat hat;
        public Skin skin;
        public byte color;
        public Pet pet;
        public byte unknown;
        public static List<RpcUpdateGameData> Deserialize(HazelBinaryReader reader)
        {
            var items = new List<RpcUpdateGameData>();
            
            while (reader.HasBytesLeft())
            {
                var msg = new RpcUpdateGameData();
                var length = reader.ReadInt16();
                var body = new HazelBinaryReader(reader.ReadBytes(length));
                reader.ReadByte();

                msg.playerId = body.ReadByte();
                msg.name = body.ReadString();

                msg.color = body.ReadByte();
                msg.hat = (Hat)body.ReadByte();
                msg.pet = (Pet)body.ReadByte();
                msg.skin = (Skin)body.ReadByte();
                msg.unknown = body.ReadByte();

                items.Add(msg);
            }

            return items;
        }
    }
}
