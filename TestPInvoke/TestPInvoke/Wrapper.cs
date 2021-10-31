using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestPInvoke
{
    internal class Wrapper
    {
        [DllImport("DynamicLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Matches(byte[] buffer, int bufferSize, int[] indexArray, int indexSize);
    }
}
