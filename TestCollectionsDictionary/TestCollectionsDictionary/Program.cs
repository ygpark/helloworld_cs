using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCollectionsDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("내용", 1);
            ;
            Console.WriteLine(dic["내용"]);
            Console.WriteLine(dic.ContainsKey("앱"));
        }
    }
}
