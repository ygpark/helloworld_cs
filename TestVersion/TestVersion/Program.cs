using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TestVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"타이틀   : {Version.AssemblyTitle}");
            Console.WriteLine($"프로덕트 : {Version.AssemblyProduct}");
            Console.WriteLine($"버전1  : v{Version.AssemblyVersionBig2}");
            Console.WriteLine($"버전2  : v{Version.AssemblyVersion}");
            Console.WriteLine($"빌드일 : {Version.AssemblyBuildDate}");
            Console.WriteLine($"저작권 : {Version.AssemblyCopyright}");
            Console.WriteLine($"회사 : {Version.AssemblyCompany}");
            Console.WriteLine($"설명 : {Version.AssemblyDescription}");
        }
    }
}
