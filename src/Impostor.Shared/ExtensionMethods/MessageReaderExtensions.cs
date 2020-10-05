using Hazel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared
{
    public static class MessageReaderExtensions
    {
        public static HazelBinaryReader GetHazelReader(this MessageReader reader)
        {
            var data = reader.ReadBytes(reader.Length);
            var mem = new MemoryStream(data);
            return new HazelBinaryReader(mem);
        }
    }
}
