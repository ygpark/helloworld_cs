using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Recovery.FileSystem.Hikvision;
using GhostYak.IO.RawDiskDrive;
using Recovery.Video.Base;

namespace TestHikvisionMBR
{
    class Program
    {
        /*
         * 채널 역할의 블록번호 구하는 공식
         * (stream.Position - HikvisionMasterSector.OffsetToVideoDataArea) / HikvisionMasterSector.DatablockSize
         * 
         * 
         * 
         */
        static void Main(string[] args)
        {
            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            //string solutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //string path = Path.Combine(projectDirectory, "DS-7204HQHI-F1_MasterRecord.dd");
            //string path = Path.Combine(projectDirectory, @"D:\test\DS-7204HQHI-F1_중간부터(BlockInfo계산용).dd");
            //DS-7204HQHI-F1_중간부터(BlockInfo계산용).dd

            PhysicalStorage pe = new PhysicalStorage(2);
            
            
            HikvisionFileSystem fs = new HikvisionFileSystem(pe.OpenRead());
            DatabaseBase db = new DatabaseBase(@"D:\test\20211001_DS-7208HQHI(아이유쉘) - rebuild7.db");
            db.Open();
            db.BeginTransaction();
            int aff = 0;
            foreach (var item in fs.ChannelInfoMap)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"UPDATE [data] SET [channel] = '{item.Channel}' ");
                sql.Append($"    WHERE [offset] >= {item.StreamStartOffset} and [offset] < {item.StreamEndOffset}; ");
                aff += db.ExecuteNonQuery(sql.ToString());
                
            }
            Console.WriteLine($"Updated: {aff}");
            db.Commit();

            if (fs.CanRead)
            {
                Console.WriteLine(fs.MasterSector.ToString(true));
                Console.WriteLine(fs.MasterSector.CanRead);
            }
            else
            {
                Console.WriteLine("Failure");
            }
            Console.ReadKey();
        }
    }
}
