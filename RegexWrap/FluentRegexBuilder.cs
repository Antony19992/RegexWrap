using RegexWrap.Components.Groups;
using RegexWrap.Core;
using RegexWrap.Factories;
using System.Text.RegularExpressions;

namespace RegexWrap
{
    public class FluentRegexBuilder
    {
        private readonly IPatternBuilder _patternBuilder;

        private FluentRegexBuilder(IPatternBuilder? patternBuilder = null)
        {
            _patternBuilder = patternBuilder ?? new PatternBuilder();
        }

        public static FluentRegexBuilder StartPattern() => new FluentRegexBuilder();

        public FluentRegexBuilder JustNumbers()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateNumber());
            return this;
        }

        public FluentRegexBuilder JustLetters()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateLetter());
            return this;
        }

        public FluentRegexBuilder WhiteSpace()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateWhitespace());
            return this;
        }

        public FluentRegexBuilder AnyChar()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateAnyChar());
            return this;
        }

        public FluentRegexBuilder Lit(string literal)
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateLiteral(literal));
            return this;
        }

        public FluentRegexBuilder Repeat(int count)
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateRepeat(count));
            return this;
        }

        public FluentRegexBuilder Repeat(int min, int max)
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateRepeat(min, max));
            return this;
        }

        public FluentRegexBuilder Optional()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateOptional());
            return this;
        }

        public FluentRegexBuilder OneOrMore()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateOneOrMore());
            return this;
        }

        public FluentRegexBuilder ZeroOrMore()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateZeroOrMore());
            return this;
        }

        public FluentRegexBuilder Group(Action<FluentRegexBuilder> inner, string? name = null)
        {
            var group = RegexComponentFactory.CreateGroup(name);
            var nestedBuilder = new FluentRegexBuilder();
            inner(nestedBuilder);
            
            var nestedPattern = nestedBuilder.ReturnAsString();
            if (!string.IsNullOrEmpty(nestedPattern))
            {
                var nestedComponents = ParsePattern(nestedPattern);
                foreach (var component in nestedComponents)
                {
                    group.AddComponent(component);
                }
            }
            
            _patternBuilder.AddComponent(group);
            return this;
        }

        public FluentRegexBuilder NonCapturingGroup(Action<FluentRegexBuilder> inner)
        {
            var group = RegexComponentFactory.CreateNonCapturingGroup();
            var nestedBuilder = new FluentRegexBuilder();
            inner(nestedBuilder);
            
            var nestedPattern = nestedBuilder.ReturnAsString();
            if (!string.IsNullOrEmpty(nestedPattern))
            {
                var nestedComponents = ParsePattern(nestedPattern);
                foreach (var component in nestedComponents)
                {
                    group.AddComponent(component);
                }
            }
            
            _patternBuilder.AddComponent(group);
            return this;
        }

        public FluentRegexBuilder Or()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateAlternation());
            return this;
        }

        public FluentRegexBuilder StartOfLine()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateStartOfLine());
            return this;
        }

        public FluentRegexBuilder EndOfLine()
        {
            _patternBuilder.AddComponent(RegexComponentFactory.CreateEndOfLine());
            return this;
        }

        public string ReturnAsString() => _patternBuilder.BuildPattern();

        public Regex ReturnAsRegex(RegexOptions options = RegexOptions.None)
            => _patternBuilder.BuildRegex(options);

        private static List<IRegexComponent> ParsePattern(string pattern)
        {
            // Simplified pattern parsing - in a real implementation, this would be more sophisticated
            var components = new List<IRegexComponent>();
            
            if (!string.IsNullOrEmpty(pattern))
            {
                components.Add(RegexComponentFactory.CreateLiteral(pattern));
            }
            
            return components;
        }
    }
}