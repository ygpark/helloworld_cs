using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTools;

namespace UnitTestRegexTools
{
    [TestClass]
    public class UnitTestRegexBin
    {
        [TestMethod]
        public void TestMethod1()
        {
            var data = new TestByteArray256Len();

            for (int i = 0; i < 256; i++)
            {
                string pattern = $"\\x{i.ToString("X2", new CultureInfo("ko-kr"))}";
                Match m = RegexBin.Match(pattern, data.Array);
                Assert.IsTrue(i == m.Index, "1 byte씩 테스트중 오류");
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var data = new TestByteArray256Len();
            byte[] arr = data.Array;
            int index = 0x33;

            arr[index + 0] = 0x00;
            arr[index + 1] = 0x00;
            arr[index + 2] = 0x00;
            arr[index + 3] = 0x00;
            arr[index + 4] = 0x01;
            arr[index + 5] = 0x67;

            string pattern = "\x00\x00\x00\x00\x01\x67";
            Match m1 = RegexBin.Match(pattern, arr);
            Assert.IsTrue(m1.Index == index, "SPS를 검색하는 경우");
        }


        [TestMethod]
        public void TestMethod3()
        {
            var data = new TestByteArray256Len();
            byte[] arr = data.Array;
            int index = 0x33;

            //
            arr[index + 0] = 0x00; //0x33
            arr[index + 1] = 0x00; //0x34
            arr[index + 2] = 0x00; //0x35
            arr[index + 3] = 0x00; //0x36
            arr[index + 4] = 0x01; //0x37
            arr[index + 5] = 0x67; //0x38

            string pattern = "\x00\x00\x00\x00\x01\x67.\x3A";
            Match m1 = RegexBin.Match(pattern, arr);
            Assert.IsTrue(m1.Index == index, "Hex값과 .리터럴이 섞이는 경우");
        }

        [TestMethod]
        public void TestMethod4()
        {
            var data = new TestByteArray256Len();
            byte[] arr = data.Array;
            int index = 0x33;

            arr[index + 0] = 0x00; //0x33
            arr[index + 1] = 0x00; //0x34
            arr[index + 2] = 0x00; //0x35
            arr[index + 3] = 0x00; //0x36
            arr[index + 4] = 0x01; //0x37
            arr[index + 5] = 0x67; //0x38

            string pattern = "\x00\x00\x00\x00\x01\x67.{1,1}\x3A";
            Match m1 = RegexBin.Match(pattern, arr);
            Assert.IsTrue(m1.Index == index, "Hex값과 .리터럴이 Min, Max를 가지는 경우1");
        }

        [TestMethod]
        public void TestMethod5()
        {
            var data = new TestByteArray256Len();
            byte[] arr = data.Array;
            int index = 0x33;

            //
            arr[index + 0] = 0x00; //0x33
            arr[index + 1] = 0x00; //0x34
            arr[index + 2] = 0x00; //0x35
            arr[index + 3] = 0x00; //0x36
            arr[index + 4] = 0x01; //0x37
            arr[index + 5] = 0x67; //0x38

            string pattern = "\x00\x00\x00\x00\x01\x67.{16}\x49";
            Match m1 = RegexBin.Match(pattern, arr);
            Assert.IsTrue(m1.Index == index, "Hex값과 .리터럴의 길이가 긴 경우");
        }

        [TestMethod]
        public void TestMethod6()
        {
            var data = new TestByteArray256Len();
            string pattern = "[\x00\x10\x20]";
            MatchCollection m1 = RegexBin.Matchs(pattern, data.Array);

            Assert.IsTrue(m1.Count == 3, "Matchs를 이용한 다중 검색(카운트)");
            Assert.IsTrue(m1[0].Index == 0x00, "Matchs를 이용한 다중 검색(첫번째 값)");
            Assert.IsTrue(m1[1].Index == 0x10, "Matchs를 이용한 다중 검색(두번째 값)");
            Assert.IsTrue(m1[2].Index == 0x20, "Matchs를 이용한 다중 검색(세번째 값)");
        }

    }
}
