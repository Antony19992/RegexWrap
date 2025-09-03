using RegexWrap.Core;
using System.Text.RegularExpressions;

namespace RegexWrap.Factories
{
    public class DefaultRegexFactory : IRegexFactory
    {
        public Regex CreateRegex(string pattern, RegexOptions options = RegexOptions.None)
        {
            return new Regex(pattern, options);
        }

        public Regex CreateCompiledRegex(string pattern, RegexOptions options = RegexOptions.Compiled)
        {
            return new Regex(pattern, options | RegexOptions.Compiled);
        }
    }
}