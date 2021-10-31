using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPInvoke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] mem = new byte[100 * 1024 * 1024];
            int[] index = new int[100];
            mem[0] = 5;

            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < 10; i++)
            {
                watch.Reset();
                watch.Start();
                Wrapper.Matches(mem, mem.Length, index, index.Length);
                watch.Stop();
                Console.WriteLine($"(소요시간: {watch.Elapsed})");
            }

            //Marshal.Copy(rst, dataRd, 0, 100);

            //Console.WriteLine($"Matches len: {len}");
            //Console.Write($"dataRd[0]: {dataRd[0]}");
            Console.ReadLine();
        }
    }
}
