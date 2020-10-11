using Impostor.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

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

        public static List<T> ReadMessageList<T>(this HazelBinaryReader reader, Func<HazelBinaryReader, int, T> itemreader)
        {
            var count = reader.ReadPackedInt32();
            var result = new List<T>();
            for (int i = 0; i < count; i++)
            {
                int id;
                var msg = reader.ReadMessage(out id);
                result.Add(itemreader(msg, id));
            }
            return result;
        }

        public static Vector2 ReadLerpVector2(this HazelBinaryReader binRdr, FloatRange xrange, FloatRange yrange)
        {
            float v = (float)binRdr.ReadUInt16() / 65535f;
            float v2 = (float)binRdr.ReadUInt16() / 65535f;
            return new Vector2(xrange.Lerp(v), yrange.Lerp(v2));
        }
    }
}
