using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery.FileSystem.Hikvision
{
    public class ChannelInfo
    {
        public int Channel { get; set; }
        public long StreamStartOffset { get; set; }
        public long StreamEndOffset { get; set; }

        public override string ToString()
        {
            return $"{{{Channel}, {StreamStartOffset}, {StreamEndOffset}}}";
        }
    }
}
