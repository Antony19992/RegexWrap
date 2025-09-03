using System.Text;
using System.Text.RegularExpressions;

namespace RegexWrap
{
    public class RegexBuilder
    {
        private readonly FluentRegexBuilder _fluentBuilder;

        private RegexBuilder() 
        { 
            _fluentBuilder = FluentRegexBuilder.StartPattern();
        }

        public static RegexBuilder StartPattern() => new();

        public RegexBuilder JustNumbers()
        {
            _fluentBuilder.JustNumbers();
            return this;
        }

        public RegexBuilder JustLetters()
        {
            _fluentBuilder.JustLetters();
            return this;
        }

        public RegexBuilder WhiteSpace()
        {
            _fluentBuilder.WhiteSpace();
            return this;
        }

        public RegexBuilder AnyChar()
        {
            _fluentBuilder.AnyChar();
            return this;
        }

        public RegexBuilder Lit(string literal)
        {
            _fluentBuilder.Lit(literal);
            return this;
        }

        public RegexBuilder Repeat(int n)
        {
            _fluentBuilder.Repeat(n);
            return this;
        }

        public RegexBuilder Repeat(int min, int max)
        {
            _fluentBuilder.Repeat(min, max);
            return this;
        }

        public RegexBuilder Optional()
        {
            _fluentBuilder.Optional();
            return this;
        }

        public RegexBuilder OneOrMore()
        {
            _fluentBuilder.OneOrMore();
            return this;
        }

        public RegexBuilder ZeroOrMore()
        {
            _fluentBuilder.ZeroOrMore();
            return this;
        }

        public RegexBuilder Group(Action<RegexBuilder> inner)
        {
            _fluentBuilder.Group(builder => 
            {
                var legacyBuilder = new RegexBuilder();
                inner(legacyBuilder);
                // Convert legacy builder pattern to new builder
                var pattern = legacyBuilder.ReturnAsString();
                if (!string.IsNullOrEmpty(pattern))
                {
                    builder.Lit(pattern);
                }
            });
            return this;
        }

        public RegexBuilder NonCapturingGroup(Action<RegexBuilder> inner)
        {
            _fluentBuilder.NonCapturingGroup(builder => 
            {
                var legacyBuilder = new RegexBuilder();
                inner(legacyBuilder);
                // Convert legacy builder pattern to new builder
                var pattern = legacyBuilder.ReturnAsString();
                if (!string.IsNullOrEmpty(pattern))
                {
                    builder.Lit(pattern);
                }
            });
            return this;
        }

        public RegexBuilder Or()
        {
            _fluentBuilder.Or();
            return this;
        }

        public RegexBuilder StartOfLine()
        {
            _fluentBuilder.StartOfLine();
            return this;
        }

        public RegexBuilder EndOfLine()
        {
            _fluentBuilder.EndOfLine();
            return this;
        }

        public string ReturnAsString() => _fluentBuilder.ReturnAsString();

        public Regex ReturnAsRegex(RegexOptions options = RegexOptions.None)
            => _fluentBuilder.ReturnAsRegex(options);
    }
}
