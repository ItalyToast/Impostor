using System.Net;
using Hazel;

namespace Impostor.Shared.Innersloth.Messages
{
    public class Redirect
    {
        public byte[] adress;
        public int port;

        public static Redirect Deserialize(HazelBinaryReader reader)
        {
            var msg = new Redirect();
            msg.adress = reader.ReadBytes(4);
            msg.port = reader.ReadUInt16();
            return msg;
        }
    }
}