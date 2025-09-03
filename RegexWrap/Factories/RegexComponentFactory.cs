using RegexWrap.Components;
using RegexWrap.Components.Anchors;
using RegexWrap.Components.Characters;
using RegexWrap.Components.Groups;
using RegexWrap.Components.Quantifiers;
using RegexWrap.Core;

namespace RegexWrap.Factories
{
    public static class RegexComponentFactory
    {
        public static IRegexComponent CreateNumber() => new NumberComponent();
        
        public static IRegexComponent CreateLetter() => new LetterComponent();
        
        public static IRegexComponent CreateWhitespace() => new WhitespaceComponent();
        
        public static IRegexComponent CreateAnyChar() => new AnyCharComponent();
        
        public static IRegexComponent CreateLiteral(string literal) => new LiteralComponent(literal);
        
        public static IRegexComponent CreateRepeat(int count) => new RepeatComponent(count);
        
        public static IRegexComponent CreateRepeat(int min, int max) => new RepeatComponent(min, max);
        
        public static IRegexComponent CreateOptional() => new OptionalComponent();
        
        public static IRegexComponent CreateOneOrMore() => new OneOrMoreComponent();
        
        public static IRegexComponent CreateZeroOrMore() => new ZeroOrMoreComponent();
        
        public static GroupComponent CreateGroup(string? name = null) => new GroupComponent(name);
        
        public static NonCapturingGroupComponent CreateNonCapturingGroup() => new NonCapturingGroupComponent();
        
        public static IRegexComponent CreateStartOfLine() => new StartOfLineComponent();
        
        public static IRegexComponent CreateEndOfLine() => new EndOfLineComponent();
        
        public static IRegexComponent CreateAlternation() => new AlternationComponent();
        
        // .NET 9 specific: SearchValues for high-performance character set matching
        public static SearchValuesComponent CreateSearchValues(ReadOnlySpan<char> allowedChars, bool negated = false) 
            => new SearchValuesComponent(allowedChars, negated);
    }
}