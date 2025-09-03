using System.Text.RegularExpressions;
using Xunit;

namespace RegexWrap.Tests
{
    public class BackwardCompatibilityTests
    {
        [Fact]
        public void OriginalRegexBuilder_ShouldWork()
        {
            var regex = RegexBuilder.StartPattern()
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
        public void OriginalRegexBuilder_GroupPattern_ShouldWork()
        {
            var regex = RegexBuilder.StartPattern()
                .Group(r => r.JustLetters().OneOrMore())
                .Lit("@")
                .Group(r => r.JustLetters().OneOrMore())
                .ReturnAsRegex();

            Assert.Matches(regex, "test@domain");
        }
    }
}