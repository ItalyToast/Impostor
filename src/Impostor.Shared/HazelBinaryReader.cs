using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared
{
    public class HazelBinaryReader : BinaryReader
    {
        public HazelBinaryReader(byte[] buffer) : base(new MemoryStream(buffer))
        {
        }
        public HazelBinaryReader(Stream  stream) : base(stream)
        {
        }
        public int ReadPackedInt32()
        {
            return Read7BitEncodedInt();
        }

        public uint ReadPackedUInt32()
        {
            return (uint)Read7BitEncodedInt();
        }

        public override byte[] ReadBytes(int count)
        {
            var result = base.ReadBytes(count);
            if (result.Length != count)
            {
                throw new Exception("Read past buffer");
            }
            return result;
        }

        public byte[] ReadBytesAndSize()
        {
            int len = ReadPackedInt32();
            return ReadBytes(len);
        }

        public List<T> ReadList<T>(Func<HazelBinaryReader, T> reader)
        {
            var count = ReadPackedInt32();
            var result = new List<T>();
            for (int i = 0; i < count; i++)
            {
                result.Add(reader(this));
            }
            return result;
        }

        
    }
}
