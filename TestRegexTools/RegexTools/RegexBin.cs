using System.Text;
using System.Text.RegularExpressions;

namespace RegexTools
{

    /// <summary>
    /// 주의! RegexExByteArray class는 메모리 부족 오류를 발생시킬 수 있으므로 충분히 테스트 후에 사용해야한다.
    /// byte[]에 대한 10MB, 100MB, 500MB 단위의 반복적인 실행 테스트가 필요하다.
    /// 실제 사용시 10MB정도면 충분할 것으로 생각한다.
    /// 
    /// 나는 2019년 프로젝트를 진행할 때 많은 테스트 끝에 결국 RE2.NET 라이브러리를 사용했다.
    /// RE2.NET을 사용하므로써 메모리 부족 오류에서 벗어날 수 있었고, 약간의 속도적인 이득을 볼 수 있었다.
    /// </summary>
    public static class RegexBin
    {
        public static Match Match(string pattern, byte[] data)
        {
            Regex regex = new Regex(pattern, RegexOptions.Multiline);
            string input = Encoding.GetEncoding("latin1").GetString(data);
            Match m = regex.Match(input);
            return m;
        }

        public static MatchCollection Matchs(string pattern, byte[] data)
        {
            Regex regex = new Regex(pattern, RegexOptions.Multiline);
            string input = Encoding.GetEncoding("latin1").GetString(data);
            return regex.Matches(input);
        }
    }
}
