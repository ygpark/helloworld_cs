using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMpeg2PS
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] dd = new byte[] { 0x00, 0x00, 0x01, 0xBA, 0x4F, 0x4A, 0x36, 0xF8, 0x14, 0x01, 
                                     0x02, 0x8F, 0x63, 0xFE, 0xFF, 0xFF, 0x15, 0x67, 0x59, 0xEA, 
                                     0x00, 0x00, 0x01, 0xBC, 0x00, 0x5E, 0xE0, 0xFF, 0x00, 0x24,
                                     0x40, 0x0E, 0x48, 0x4B, 0x01, 0x00, 0x15, 0x41, 0x86, 0x9E,
                                     0x3D, 0xB0, 0x00, 0xFF, 0xFF, 0xFF, 0x41, 0x12, 0x48, 0x4B,
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                     0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x24, 0xE0,
                                     0x00, 0x1C, 0x42, 0x0E, 0x00, 0x00, 0x60, 0x00, 0x07, 0x80 };

            var ph = new PackHeaderDecoder(dd);
            Console.WriteLine("{0:X}", ph.pack_stuffing_length());

            Console.WriteLine(ph.ToString());
        }

        class PackHeaderDecoder
        {
            byte[] _data = null;
            public PackHeaderDecoder(byte[] data)
            {
                _data = data;

                //http://dvd.sourceforge.net/dvdinfo/packhdr.html
                if (_data.Length < 14)
                    throw new Exception("Invalid PackHeader Length");

                if ( 0x44/*0100 0100*/ != (_data[4] & 0xC4/*1100 0100*/) )
                    throw new Exception("Invalid PackHeader Fixed Bit");

                if (0x04/*0000 0100*/ != (_data[6] & 0x04/*0000 0100*/))
                    throw new Exception("Invalid PackHeader Fixed Bit");

                if (0x04/*0000 0100*/ != (_data[8] & 0x04/*0000 0100*/))
                    throw new Exception("Invalid PackHeader Fixed Bit");

                if (0x01/*0000 0001*/ != (_data[9] & 0x01/*0000 0001*/))
                    throw new Exception("Invalid PackHeader Fixed Bit");

                if (0x03/*0000 0011*/ != (_data[12] & 0x03/*0000 0011*/))
                    throw new Exception("Invalid PackHeader Fixed Bit");
            }

            public byte[] startCode()
            {
                byte[] r = new byte[3];
                Array.Copy(_data, r, 3);
                return r;
            }

            public int PACK_identifier()
            {
                return (int)_data[3];
            }

            public long SystemClockKHz()
            {
                long scr;
                long scr_ext;

                scr  = ((long)_data[4] & ((long)7 << 3/*111000*/)) << 27; //SRC 32..30
                scr += (((long)_data[4] & (long)3/*0011*/) << 28);               //SRC 29..28
                scr += (long)_data[5] << 20;                              //SRC 27..20
                scr += ((_data[6] & 0xF8/*1111 1000*/) << 12);      //SRC 19..15
                scr += ((_data[6] & 0x03/*0000 0011*/) << 13);      //SRC 14..13
                scr += _data[7] << 5;                               //SRC 12..5
                scr += _data[8] >> 3;                               //SRC 4..0

                scr_ext = (_data[8] & 3) << 7;
                scr_ext += (_data[9] & 0xFE) >> 1;


                return scr*300 + scr_ext;//KHz
            }

            public long Program_Mux_Rate()
            {
                long r;

                r = _data[10] << 14;
                r += _data[11] << 6;
                r += _data[12] >> 2;

                return r;
            }

            public int pack_stuffing_length()
            {
                return (int)_data[13] & 0x07/*0b111*/;
            }

            public byte[] pack_stuffing()
            {
                int len = pack_stuffing_length()*8;
                byte[] ps = new byte[len];
                Array.Copy(_data, 14, ps, 0, len);
                return ps;
            }

            public override string ToString()
            {
                string r = string.Empty;
                byte[] sCode = startCode();
                byte[] ps = pack_stuffing();
                r += $"startCode: 0x{sCode[0]:X2}{sCode[1]:X2}{sCode[2]:X2}\n";
                r += $"PACK_identifier: 0x{PACK_identifier():X}\n";
                r += $"SystemClock: {SystemClockKHz()}KHz\n";
                r += $"Program_Mux_Rate: {Program_Mux_Rate()}\n";
                r += $"pack_stuffing_length: {pack_stuffing_length()}\n";
                r += $"pack_stuffing: ";
                for(int i =0; i < ps.Length; i++)
                {
                    r += string.Format("{0:X2} ", ps[i]);
                }
                r += "\n";


                return r;
            }
        }
    }
}
