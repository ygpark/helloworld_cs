using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Recovery.FileSystem.Hikvision
{
    //
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class BlockInfo
    {
        //000h: -- -- -- -- -- -- -- --   -- -- -- -- -- -- -- --
        //010h: -- -- -- -- -- -- -- --   -- -- -- -- -- -- -- --
        //020h: -- -- -- -- -- -- -- --   -- -- -- -- -- -- -- --
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        private byte[] unknown_0;

        //030h: 00 00 00 00 00 00 00 00   -- -- -- -- -- -- -- --
        private long _endUnixTime;
        //030h: -- -- -- -- -- -- -- --   00 00 00 00 00 00 00 00
        private long _startunixTime;

        //040h: 00 00 00 00 00 00 00 00   -- -- -- -- -- -- -- --
        private long _unknown_40h;

        //040h: -- -- -- -- -- -- -- --   00 00 00 00 -- -- -- --
        private uint _startOffset;

        //040h: -- -- -- -- -- -- -- --   -- -- -- -- 00 00 00 00
        private uint _endOffset;


        //050h: -- -- -- -- -- -- -- --   -- -- -- -- -- -- -- --
        //1F0h: -- -- -- -- -- -- -- --   -- -- -- -- -- -- -- --
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 432)]
        private byte[] unknown_50h;

        public long StartOffset { get { return (long)_startOffset; } }
        public long EndOffset { get { return (long)_endOffset; } }

        public DateTime StartTime {
            get 
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                epoch = epoch.AddSeconds(_startunixTime);
                //epoch = epoch.AddHours(9);  //TODO: 이거 +9 해야 하나 확인... 사용 안하는 값이라서 아직은 할 필요 없음.
                return epoch;
            }
        }

        public DateTime EndTime
        {
            get
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                epoch = epoch.AddSeconds(_endUnixTime);
                //epoch = epoch.AddHours(9);  //TODO: 이거 +9 해야 하나 확인... 사용 안하는 값이라서 아직은 할 필요 없음.
                return epoch;
            }
        }


        public BlockInfo()
        {
            
        }

    }
}