using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestRegex
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
            Test2();
        }

        static void Test1()
        {
            Console.WriteLine("주소 테스트");
            string pattern = @"^http://dn-v.talk.kakao.com.*\.(jpg|jpeg|png|mp4)$";
            string[] source = new string[]
                {
                    "http://dn-v.talk.kakao.com/talkv/wonzTAzl7a/2Os0fp8kuOToZvyShICCp1/talkv_high.mp4",
                    "http://www.kalsdfj.jpeg",
                    "http://www.kalsdfj.mp4",
                    "http://www.kalsdfj.mp4.content",
                    "http://www.kalsdfj.thumb",
                };
            Regex re = new Regex(pattern);
            for (int i = 0; i < source.Length; i++)
            {
                Console.WriteLine(re.IsMatch(source[i]));
            }

            Console.WriteLine("---------------------------");
        }
        static void Test2()
        {
            Console.WriteLine("내용 테스트");
            string pattern = @"http://[a-zA-Z0-9\-]+.talk.kakao.com.*\.(jpg|jpeg|png|mp4)";
            string[] source = new string[]
                {
                    "사진       http://dn-v.talk.kakao.com/talkv/wonzTAzl7a/2Os0fp8kuOToZvyShICCp1/talkv_high.jpg",
                    "사진       http://dn-v.talk.kakao.com/talkv/wonzTAzl7a/2Os0fp8kuOToZvyShICCp1/talkv_high.jpeg",
                    "사진       http://dn-v.talk.kakao.com/talkv/wonzTAzl7a/2Os0fp8kuOToZvyShICCp1/talkv_high.png",
                    "동영상     http://dn-v.talk.kakao.com/talkv/wonzTAzl7a/2Os0fp8kuOToZvyShICCp1/talkv_high.mp4\nhttp://dn-v.talk.kakao.com/talkv/wonzTAzl7a/2Os0fp8kuOToZvyShICCp1/talkv_high.jpeg",
                    "사진 256장 http://th-m2.talk.kakao.com/th/talkm/oXEMJM3rK4/18gmatWpofXqryvW8TRWx1/i_pe2nb3p54p9v_99x120.jpeg",
                    "http://www.kalsdfj.mp4",
                    "http://www.kalsdfj.mp4.content",
                    "http://www.kalsdfj.thumb",
                };
            Regex re = new Regex(pattern);
            for (int i = 0; i < source.Length; i++)
            {
                Console.WriteLine(re.IsMatch(source[i]));
            }

            Console.WriteLine("---------------------------");
        }
    }
}
