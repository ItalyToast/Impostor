using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Messages
{
    public class ReselectServer
    {
        public List<Server> servers;

        public class Server
        {
            public string name;
            public byte[] ip;
            public ushort port;
            public byte[] unknown;
        }

        public static ReselectServer Deserialize(HazelBinaryReader reader)
        {
            var msg = new ReselectServer();

            var u1 = reader.ReadByte();
            msg.servers = reader.ReadMessageList((read, id) => 
            {
                var server = new Server();
                server.name = read.ReadString();
                server.ip = read.ReadBytes(4);
                server.port = read.ReadUInt16();
                server.unknown = read.ReadBytes(2);
                return server;
            });

            return msg;
        }
    }
}
