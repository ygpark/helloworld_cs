using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMath
{
    class Program
    {
        static void Main(string[] args)
        {
            byte b = byte.Parse("0B", NumberStyles.HexNumber);
            Console.WriteLine(b.ToString());
            Math_Ceiling();
            Math_Floor();
        }
        static void Math_Ceiling()
        {
            double v = default;
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("천장(올림) 함수(Math.Ceiling)");

            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Ceiling(v)}");
            v = 0.1;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Ceiling(v)}");
            v = 0.5;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Ceiling(v)}");
            v = 0.9;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Ceiling(v)}");
            v = 1;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Ceiling(v)}");
            Console.WriteLine("-----------------------------------------------");
        }

        static void Math_Floor()
        {
            double v = default;
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("바닥(내림) 함수(Math.Floor)");

            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Floor(v)}");
            v = 0.1;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Floor(v)}");
            v = 0.5;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Floor(v)}");
            v = 0.9;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Floor(v)}");
            v = 1;
            Console.WriteLine($"Math.Ceiling({v.ToString()}) = {Math.Floor(v)}");
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
