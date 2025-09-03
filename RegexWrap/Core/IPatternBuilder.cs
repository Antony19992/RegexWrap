using System.Text.RegularExpressions;

namespace RegexWrap.Core
{
    public interface IPatternBuilder
    {
        IPatternBuilder AddComponent(IRegexComponent component);
        string BuildPattern();
        Regex BuildRegex(RegexOptions options = RegexOptions.None);
        void Clear();
    }
}