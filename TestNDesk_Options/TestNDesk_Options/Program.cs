using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;

namespace TestNDesk_Options
{
    class Program
    {
        static int _verbosity = 0;
        static bool _show_help = false;
        static string _file_name = string.Empty;
        static string _expression = string.Empty;

        public static void Main(string[] args)
        {
            OptionSet p = new OptionSet()
              .Add("h|help|?", "도움말",  v => _show_help = true)
              .Add("l|list", "모든 물리 디스크 목록 출력", v => ShowAllDisks())
              .Add("v|verbose", "상세 정보 출력 모드", v => ++_verbosity)
              .Add("i|ifile=", "입력 파일 이름 또는 물리 디스크 이름", v => _file_name = v.Trim())
              .Add("e|efile=", "정규표현식이 저장된 텍스트 파일(1 LINE)", v => _expression = v)
              .Add("s|skip", "건너띄기", v => Console.WriteLine(v));


            try
            {
                var z = p.Parse(args);
                //p.Parse(new string[] { "-help", "-l", "--v", "/v", "-name=A", "/name", "B", "extra" });
                Debug(_expression);
                foreach (var item in z)
                {
                    Debug(item);
                }
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                return;
            }

            if (_show_help)
            {
                ShowHelp(p);
                return;
            }

            //ShowHelp(p);

        }

        static void ShowAllDisks()
        {
            Debug("ShowAllDisks()");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($@"\\.\PHYSICALDRIVE{i}");
            }
        }
        static void ShowHelp(OptionSet p)
        {
            Debug("ShowHelp(OptionSet p)");
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        static void Debug(string format, params object[] args)
        {
            if (_verbosity > 0)
            {
                Console.Write("# ");
                Console.WriteLine(format, args);
            }
        }
    }
}
