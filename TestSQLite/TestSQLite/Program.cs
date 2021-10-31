using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLite
{
    class Program
    {
        static void JustPrint(string fileName)
        {
            string ConnectionString = $"Data Source={fileName};Version=3";

            var Command = new SQLiteCommand(new SQLiteConnection(ConnectionString));
            Command.Connection.Open();
            Command.CommandText = "select * from recvmsg7;";
            var reader = Command.ExecuteReader();

            while (reader.Read())
            {
                string FTitle = reader["FTitle"].ToString();
                Console.WriteLine(FTitle);
            }
        }

        static void TestEncoding1(string fileName)
        {
            string ConnectionString = $"Data Source={fileName};Version=3";

            var Command = new SQLiteCommand(new SQLiteConnection(ConnectionString));
            Command.Connection.Open();
            Command.CommandText = "select * from recvmsg7;";
            var reader = Command.ExecuteReader();

            reader.Read();
            string FTitle = reader["FTitle"].ToString();
            byte[] buffer = Encoding.ASCII.GetBytes(FTitle);

            var encodings = Encoding.GetEncodings();
            foreach (var item in encodings)
            {
                string fixString = item.GetEncoding().GetString(buffer);
                Console.WriteLine(item.DisplayName + " : " + fixString);
            }
        }

        static void TestEncoding2(string fileName)
        {
            string ConnectionString = $"Data Source={fileName};Version=3";
            var conn = new SQLiteConnection(ConnectionString);
            var Command = new SQLiteCommand(conn);
            Command.Connection.Open();

            Command.CommandText = "select * from recvmsg7 where FTitle limit 0;";
            var reader = Command.ExecuteReader();

            reader.Read();
            string FTitle = reader["FTitle"].ToString();
            byte[] buffer = Encoding.GetEncoding("EUC-KR").GetBytes(FTitle);
            //byte[] buffer = Encoding.UTF8.GetBytes(FTitle);

            var encodings = Encoding.GetEncodings();
            foreach (var item in encodings)
            {
                string fixString = item.GetEncoding().GetString(buffer);
                Console.WriteLine(item.DisplayName + " : " + fixString);
            }
        }

        static void Main(string[] args)
        {
            //JustPrint(@"D:\workspace\sql\KJM7.db");
            //TestEncoding2(@"D:\workspace\sql\KJM7.db");
            TestEncoding2(@"D:\workspace\sql\65012501.db");
            //
            //TestEncoding2(@"D:\workspace\sql\KJM7.db");
        }
    }
}
