using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared
{
    public class HazelBinaryWriter : BinaryWriter
    {
        public HazelBinaryWriter(Stream stream) : base(stream)
        {
        }

        public void WritePackedInt(int value)
        {
            Write7BitEncodedInt(value);
        }
    }
}
