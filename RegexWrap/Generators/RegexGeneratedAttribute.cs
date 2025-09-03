using System.Text.RegularExpressions;

namespace RegexWrap.Generators
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RegexGeneratedAttribute : Attribute
    {
        public string Pattern { get; }
        public RegexOptions Options { get; set; } = RegexOptions.None;
        public int TimeoutMilliseconds { get; set; } = 5000;

        public RegexGeneratedAttribute(string pattern)
        {
            Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        }
    }
}