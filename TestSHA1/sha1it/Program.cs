using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sha1it
{
    
	class Program
	{
        static int _nLineNum = 1;
        static HashSet<FileInfo> hash = new HashSet<FileInfo>();


        public static void Add(FileInfo f)
        {
            hash.Add(f);
        }

        public static void Add(FileInfo[] f)
        {
            foreach (var item in f)
            {
                hash.Add(item);
            }
        }

        public static string GetLineNumber()
        {
            return _nLineNum.ToString();
        }

        public static void AddLineNumber()
        {
            _nLineNum++;
        }

        public static string SHA1SUM(string fname)
        {
            FileStream fop = File.OpenRead(fname);
            string chksum = BitConverter.ToString(System.Security.Cryptography.SHA1.Create().ComputeHash(fop));
            return chksum.Replace("-", "");
        }

        public static void Main(string[] args)
		{

            UTF8Encoding utf8bom = new UTF8Encoding(true);//CSV 한글깨짐 방지. UTF-8의 BOM 처리
            TextWriter log = Console.Error;

            if (args.Length == 0)
            {
                return;
            }

//#if DEBUG
//            for (int i = 0; i < args.Length; i++)
//            {
//                log.WriteLine($"[DEBUG] args[{i}]: {args[i]}");
//            }
//#endif

//#if DEBUG
//            log.WriteLine($"[DEBUG] args.Length={args.Length}");
//#endif

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Replace("\"", ""); //// <오류방지>  파워쉘, 폴더공백, 감쌀때 끝 따옴표가 \"로 처리. 에러 발생

                try
                {
                    // '*'이 있는 지 검사
                    if (args[i].Contains("*"))
                    {
                        string fname = Path.GetFileName(args[i]);
                        string dname = Path.GetDirectoryName(args[i]);
                        DirectoryInfo dinfo = new DirectoryInfo(dname);
                        FileInfo[] filesInDir = dinfo.GetFiles(fname, SearchOption.TopDirectoryOnly);
                        Add(filesInDir);
                    } else
                    {
                        FileInfo finfo = new FileInfo(args[i]);
                        if(finfo.Exists)
                        {
                            Add(finfo);
                        } else
                        {
                            string dname = args[i];
                            DirectoryInfo dinfo = new DirectoryInfo(dname);
                            if(dinfo.Exists)
                            {
                                FileInfo[] filesInDir = dinfo.GetFiles("*", SearchOption.AllDirectories);
                                Add(filesInDir);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    TextWriter error = Console.Error;
                    error.WriteLine($"[ERROR] {e.Message}");
                }
            }

            string outfile = "~out.csv";
            if(args.Length == 1)
            {
                DirectoryInfo dInfo = new DirectoryInfo(args[0]);
                if (dInfo.Exists)
                    outfile = dInfo.Name + ".csv";
            }
            using (FileStream fs = new FileStream(outfile, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(true)))
                {
                    Console.WriteLine($"\"연번\",\"파일명\",\"해시(SHA1)\"");
                    sw.WriteLine($"\"연번\",\"파일명\",\"해시(SHA1)\"");
                    foreach (FileInfo item in hash)
                    {
                        string currDir = Directory.GetCurrentDirectory();
                        string path = item.FullName.Replace(currDir, "");
                        Console.WriteLine($"\"{GetLineNumber()}\",\"{path}\",\"{SHA1SUM(item.FullName)}\"");
                        //sw.WriteLine($"\"{GetLineNumber()}\",\"{path}\",\"{SHA1SUM(item.FullName)}\"");
                        sw.WriteLine($"{GetLineNumber()},\"{path}\",{SHA1SUM(item.FullName)}");
                        AddLineNumber();
                    }
                }
            }
        }

    }
}