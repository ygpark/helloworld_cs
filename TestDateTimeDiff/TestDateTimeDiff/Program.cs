using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDateTimeDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTimePre = new DateTime(2021, 5, 21, 11, 01, 55);
            DateTime dateTimeCur = new DateTime(2021, 3, 21, 11, 49, 51);
            bool isDifHour = dateTimePre.Hour != dateTimeCur.Hour;
            TimeSpan dateTimeDiff = dateTimeCur - dateTimePre;

            bool hasTimeGap = 5 < Math.Abs(dateTimeDiff.TotalSeconds);

            if (isDifHour)//채널이 다르거나 시간이 1초 이상 벌어지면 연속되지 않은 영상으로 판단함.
            {
                Console.WriteLine($"isDifHour: dateTimePre.Hour:{dateTimePre.Hour}, dateTimeCur.Hour:{dateTimeCur.Hour}");
            }
            if (hasTimeGap)//채널이 다르거나 시간이 1초 이상 벌어지면 연속되지 않은 영상으로 판단함.
            {
                Console.WriteLine($"hasTimeGap: dateTimeDiff.TotalSeconds: {dateTimeDiff.TotalSeconds}");
            }



            return;
        }
    }
}
