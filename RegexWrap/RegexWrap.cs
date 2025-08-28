using System.Text;
using System.Text.RegularExpressions;

namespace RegexWrap
{
    public class RegexBuilder
    {
        private readonly StringBuilder _pattern = new();

        private RegexBuilder() { }

        public static RegexBuilder StartPattern() => new();

        public RegexBuilder JustNumbers()
        {
            _pattern.Append(@"\d");
            return this;
        }

        public RegexBuilder JustLetters()
        {
            _pattern.Append(@"[a-zA-Z]");
            return this;
        }

        public RegexBuilder WhiteSpace()
        {
            _pattern.Append(@"\s");
            return this;
        }

        public RegexBuilder AnyChar()
        {
            _pattern.Append(".");
            return this;
        }

        public RegexBuilder Lit(string literal)
        {
            _pattern.Append(Regex.Escape(literal));
            return this;
        }

        public RegexBuilder Repeat(int n)
        {
            _pattern.Append($"{{{n}}}");
            return this;
        }

        public RegexBuilder Repeat(int min, int max)
        {
            _pattern.Append($"{{{min},{max}}}");
            return this;
        }

        public RegexBuilder Optional()
        {
            _pattern.Append("?");
            return this;
        }

        public RegexBuilder OneOrMore()
        {
            _pattern.Append("+");
            return this;
        }

        public RegexBuilder ZeroOrMore()
        {
            _pattern.Append("*");
            return this;
        }

        public RegexBuilder Group(Action<RegexBuilder> inner)
        {
            var nested = new RegexBuilder();
            inner(nested);
            _pattern.Append($"({nested.ReturnAsString()})");
            return this;
        }

        public RegexBuilder NonCapturingGroup(Action<RegexBuilder> inner)
        {
            var nested = new RegexBuilder();
            inner(nested);
            _pattern.Append($"(?:{nested.ReturnAsString()})");
            return this;
        }

        public RegexBuilder Or()
        {
            _pattern.Append("|");
            return this;
        }

        public RegexBuilder StartOfLine()
        {
            _pattern.Append("^");
            return this;
        }

        public RegexBuilder EndOfLine()
        {
            _pattern.Append("$");
            return this;
        }

        public string ReturnAsString() => _pattern.ToString();

        public Regex ReturnAsRegex(RegexOptions options = RegexOptions.None)
            => new Regex(ReturnAsString(), options);
    }
}
