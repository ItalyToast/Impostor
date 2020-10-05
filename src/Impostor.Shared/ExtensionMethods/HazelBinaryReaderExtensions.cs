using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Impostor.Shared
{
    public static class HazelBinaryReaderExtensions
    {
        public static long GetBytesLeft(this HazelBinaryReader binRdr)
        {
            return binRdr.BaseStream.Length - binRdr.BaseStream.Position;
        }

        public static byte[] PeekBytes(this HazelBinaryReader binRdr, int count)
        {
            var pos = binRdr.BaseStream.Position;
            var bytes = binRdr.ReadBytes(count);
            binRdr.BaseStream.Position = pos;
            return bytes;
        }

        public static byte[] PeekToEnd(this HazelBinaryReader binRdr)
        {
            return binRdr.PeekBytes((int)binRdr.GetBytesLeft());
        }

        public static string ReadStringToEnd(this HazelBinaryReader binRdr)
        {
            return Encoding.UTF8.GetString(binRdr.ReadBytesToEnd());
        }

        public static byte[] ReadBytesToEnd(this HazelBinaryReader binRdr)
        {
            return binRdr.ReadBytes((int)binRdr.GetBytesLeft());
        }

        public static HazelBinaryReader ReadBodyToEnd(this HazelBinaryReader binRdr)
        {
            return new HazelBinaryReader(new MemoryStream(binRdr.ReadBytesToEnd()));
        }

        public static bool HasBytesLeft(this HazelBinaryReader binRdr)
        {
            return binRdr.GetBytesLeft() > 0;
        }

        public static HazelBinaryReader ReadMessage(this HazelBinaryReader binRdr, out int type)
        {
            var size = binRdr.ReadUInt16();
            type = binRdr.ReadByte();
            var data = binRdr.ReadBytes(size);
            return new HazelBinaryReader(data);
        }
    }
}
