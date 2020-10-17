using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class HelloHandshake
    {
        public ushort Nonce;
        public byte Version;
        public int ClientVersion;
        public string Name;

        public static HelloHandshake Deserialize(HazelBinaryReader reader)
        {
            var msg = new HelloHandshake();

            msg.Nonce = reader.ReadUInt16();
            msg.Version = reader.ReadByte();
            msg.ClientVersion = reader.ReadInt32();
            msg.Name = reader.ReadString();

            return msg;
        }
    }
}
