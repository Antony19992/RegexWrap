using System.Text.RegularExpressions;
using Xunit;

namespace RegexWrap.Tests
{
    public class FluentRegexBuilderTests
    {
        [Fact]
        public void FluentBuilder_BasicPattern_ShouldWork()
        {
            var regex = FluentRegexBuilder.StartPattern()
                .StartOfLine()
                .JustLetters().OneOrMore()
                .WhiteSpace()
                .JustNumbers().Repeat(2, 4)
                .EndOfLine()
                .ReturnAsRegex();

            Assert.Matches(regex, "Test 123");
            Assert.Matches(regex, "Hello 99");
            Assert.DoesNotMatch(regex, "123 Test");
        }

        [Fact]
        public void FluentBuilder_GroupPattern_ShouldWork()
        {
            var regex = FluentRegexBuilder.StartPattern()
                .Group(g => g.JustLetters().OneOrMore())
                .Lit("@")
                .Group(g => g.JustLetters().OneOrMore())
                .ReturnAsRegex();

            Assert.Matches(regex, "test@domain");
            Assert.DoesNotMatch(regex, "test@123");
        }

        [Fact]
        public void FluentBuilder_LiteralEscaping_ShouldWork()
        {
            var regex = FluentRegexBuilder.StartPattern()
                .Lit("test.regex")
                .ReturnAsRegex();

            Assert.Matches(regex, "test.regex");
            Assert.DoesNotMatch(regex, "testXregex");
        }

        [Fact]
        public void FluentBuilder_AlternationPattern_ShouldWork()
        {
            var regex = FluentRegexBuilder.StartPattern()
                .JustLetters().OneOrMore()
                .Or()
                .JustNumbers().OneOrMore()
                .ReturnAsRegex();

            Assert.Matches(regex, "abc");
            Assert.Matches(regex, "123");
            Assert.DoesNotMatch(regex, "abc123");
        }
    }
}