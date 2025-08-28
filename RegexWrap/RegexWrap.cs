using System.Text;
using System.Text.RegularExpressions;

namespace RegexWrap
{
    public class RegexWrap
    {
        private readonly StringBuilder _pattern = new();

        private RegexWrap() { }

        public static RegexWrap StartPattern() => new();

        public RegexWrap JustNumbers()
        {
            _pattern.Append(@"\d");
            return this;
        }

        public RegexWrap JustLetters()
        {
            _pattern.Append(@"[a-zA-Z]");
            return this;
        }

        public RegexWrap WhiteSpace()
        {
            _pattern.Append(@"\s");
            return this;
        }

        public RegexWrap AnyChar()
        {
            _pattern.Append(".");
            return this;
        }

        public RegexWrap Lit(string literal)
        {
            _pattern.Append(Regex.Escape(literal));
            return this;
        }

        public RegexWrap Repeat(int n)
        {
            _pattern.Append($"{{{n}}}");
            return this;
        }

        public RegexWrap Repeat(int min, int max)
        {
            _pattern.Append($"{{{min},{max}}}");
            return this;
        }

        public RegexWrap Optional()
        {
            _pattern.Append("?");
            return this;
        }

        public RegexWrap OneOrMore()
        {
            _pattern.Append("+");
            return this;
        }

        public RegexWrap ZeroOrMore()
        {
            _pattern.Append("*");
            return this;
        }

        public RegexWrap Group(Action<RegexWrap> inner)
        {
            var nested = new RegexWrap();
            inner(nested);
            _pattern.Append($"({nested.ReturnAsString()})");
            return this;
        }

        public RegexWrap NonCapturingGroup(Action<RegexWrap> inner)
        {
            var nested = new RegexWrap();
            inner(nested);
            _pattern.Append($"(?:{nested.ReturnAsString()})");
            return this;
        }

        public RegexWrap Or()
        {
            _pattern.Append("|");
            return this;
        }

        public RegexWrap StartOfLine()
        {
            _pattern.Append("^");
            return this;
        }

        public RegexWrap EndOfLine()
        {
            _pattern.Append("$");
            return this;
        }

        public string ReturnAsString() => _pattern.ToString();

        public Regex ReturnAsRegex(RegexOptions options = RegexOptions.None)
            => new Regex(ReturnAsString(), options);
    }
}
