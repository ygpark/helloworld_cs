using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GhostYak.Options;


namespace TestGhostYak_Options
{

    class Program
    {
        static void Main(string[] args)
        {
            int help = 0;
            int list = 0;
            int verbosity = 0;
            int skip = 0;
            string fileName = string.Empty;
            string expression = string.Empty;

            OptionSet o = new OptionSet();
            o.Add("h|help", "도움말", v => help++)
                .Add("l|list", "모든 물리 디스크 목록 출력", v => list++)
                .Add("v|verbose", "상세 정보 출력 모드", v => verbosity++)
                .Add("i|ifile", "입력 파일 이름 또는 물리 디스크 이름", v => fileName = v.Trim())
                .Add("e|regex", "정규표현식. -e\"regular expression\"", v => expression = v)
                .Add("s|skip", "건너띄기 할 byte 수", v => skip = int.Parse(v))
            ;

            o.Parse(args);

            if (0 < help)
            {
                ShowHelp(o);
                return;
            }

            Console.WriteLine($" -i = {fileName}");
            Console.WriteLine($" -e = {expression}");

        }

        private static void ShowHelp(OptionSet optionSet)
        {
            Console.WriteLine("사용법: command [Options]");
            Console.WriteLine("Options:");
            Console.WriteLine(optionSet.GetOptionDescriptions());
        }
    }
}
