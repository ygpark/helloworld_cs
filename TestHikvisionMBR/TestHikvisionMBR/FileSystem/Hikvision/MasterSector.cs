using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Recovery.FileSystem.Hikvision
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MasterSector
    {
        //200h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private byte[] _unknown200;

        //210h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00    HIKVISION@HANGZHOU
        //220h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private byte[] _signiture;

        //230h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        //240h: 00 00 00 00 00 00 00 00   -- -- -- -- -- -- -- --
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        private byte[] _unknown240;

        //240h: -- -- -- -- -- -- -- --   00 00 00 00 00 00 00 00
        private long _diskSize;

        //250h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        //260h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        //270h: 00 00 00 00 00 00 00 00   -- -- -- -- -- -- -- --
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        private byte[] _unknown250;
        //270h: -- -- -- -- -- -- -- --   00 00 00 00 00 00 00 00
        private long _offsetToVideoDataArea;

        //280h: 00 00 00 00 00 00 00 00   -- -- -- -- -- -- -- --
        private long _unknown280;
        //280h: -- -- -- -- -- -- -- --   00 00 00 00 00 00 00 00
        private long _dataBlockSize;

        //290h: 00 00 00 00 -- -- -- --   -- -- -- -- -- -- -- --
        private int _dataBlockCount;
        //290h: -- -- -- -- 00 00 00 00  -- -- -- -- -- -- -- --
        private int _unknown290;
        //290h: -- -- -- -- -- -- -- --   00 00 00 00 00 00 00 00
        private long _HIKBTREE1Offset;

        //2A0h: 00 00 00 00 -- -- -- --   -- -- -- -- -- -- -- --
        private int _HIKBTREE1Size;
        //2A0h: -- -- -- -- 00 00 00 00  -- -- -- -- -- -- -- --
        private int _unknown2A0;
        //2A0h: -- -- -- -- -- -- -- --   00 00 00 00 00 00 00 00
        private long _HIKBTREE2Offset;

        //2B0h: 00 00 00 00 -- -- -- --   -- -- -- -- -- -- -- --
        private int _HIKBTREE2Size;

        //2B0h: -- -- -- -- 00 00 00 00   00 00 00 00 00 00 00 00
        //2C0h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        //2D0h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        //2E0h: 00 00 00 00 00 00 00 00   00 00 00 00 00 00 00 00
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
        private byte[] _unknown2B0;

        //2F0h: 00 00 00 00 -- -- -- --   -- -- -- -- -- -- -- --
        private int _systemInitTimeUTC0;


        public string Signiture { get { return Encoding.ASCII.GetString(_signiture).Trim('\0'); } }

        public long DiskSize { get { return _diskSize; } }

        public long OffsetToVideoDataArea { get { return _offsetToVideoDataArea; } }

        public long DatablockSize { get { return _dataBlockSize; } }
        public int DatablockCount { get { return _dataBlockCount; } }

        public long HIKBTREE1Offset { get { return _HIKBTREE1Offset; } }
        public int HIKBTREE1Size { get { return _HIKBTREE1Size; } }

        public long HIKBTREE2Offset { get { return _HIKBTREE2Offset; } }
        public int HIKBTREE2Size { get { return _HIKBTREE2Size; } }

        public bool CanRead { get { return Signiture == "HIKVISION@HANGZHOU"; } }

        public DateTime SystemInitTime
        {
            get
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                epoch = epoch.AddSeconds(_systemInitTimeUTC0);
                return epoch;
            }
        }


        public int Length { get { return (int)Marshal.SizeOf(typeof(MasterSector)); } }
        public MasterSector()
        {

        }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool newLine)
        {
            string enNewLine = string.Empty;
            if (newLine)
                enNewLine = Environment.NewLine + Environment.NewLine;


            return $"Signiture: {Signiture}, {enNewLine}"
                + $"DiskSize: {DiskSize:0,0} ({GetHumanReadableSize(DiskSize)}), {enNewLine}"
                + $"OffsetToVideoDataArea: {OffsetToVideoDataArea} ({GetHumanReadableSize(OffsetToVideoDataArea)}), {enNewLine}"
                + $"DatablockSize: {DatablockSize:0,0} ({GetHumanReadableSize(DatablockSize)}), {enNewLine}"
                + $"DatablockCount: {DatablockCount:0,0}, {enNewLine}"
                + $"HIKBTREE1Offset: {HIKBTREE1Offset:0,0} ({GetHumanReadableSize(HIKBTREE1Offset)}), {enNewLine}"
                + $"HIKBTREE1Size: {HIKBTREE1Size:0,0}, ({GetHumanReadableSize(HIKBTREE1Size)}){enNewLine}"
                + $"HIKBTREE2Offset: {HIKBTREE2Offset:0,0} ({GetHumanReadableSize(HIKBTREE2Offset)}), {enNewLine}"
                + $"HIKBTREE2Size: {HIKBTREE2Size:0,0}, ({GetHumanReadableSize(HIKBTREE2Size)}){enNewLine}"
                + $"SystemInitTime: {SystemInitTime}";
        }

        public static string GetHumanReadableSize(double byteSize)
        {
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int idx = 0;
            double dHumanReadableSize = byteSize;
            for (int i = 0; i < units.Length; i++)
            {
                if (dHumanReadableSize >= (double)1000)
                {
                    dHumanReadableSize /= (double)1024;//Mib로 표시하고 싶으면 1024를 1000으로
                    idx++;
                }
            }

            dHumanReadableSize = Math.Floor(100 * dHumanReadableSize) / 100;

            return string.Format("{0:0.00}{1}", dHumanReadableSize, units[idx]);
        }
    }
}