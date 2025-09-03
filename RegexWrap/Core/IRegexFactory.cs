using System.Text.RegularExpressions;

namespace RegexWrap.Core
{
    public interface IRegexFactory
    {
        Regex CreateRegex(string pattern, RegexOptions options = RegexOptions.None);
        Regex CreateCompiledRegex(string pattern, RegexOptions options = RegexOptions.Compiled);
    }
}