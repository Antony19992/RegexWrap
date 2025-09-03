using System.Text.RegularExpressions;
using Xunit;

namespace RegexWrap.Tests
{
    /// <summary>
    /// Demonstrates proper xUnit regex assertion patterns following xUnit2008 guidelines
    /// </summary>
    public class RegexAssertionTests
    {
        [Fact]
        public void RegexAssertions_ShouldFollowxUnitGuidelines()
        {
            var emailPattern = FluentRegexBuilder.StartPattern()
                .JustLetters().OneOrMore()
                .Lit("@")
                .JustLetters().OneOrMore()
                .Lit(".")
                .JustLetters().Repeat(2, 4)
                .ReturnAsRegex();

            // ✅ Correct: Use Assert.Matches for positive regex tests
            Assert.Matches(emailPattern, "user@domain.com");
            Assert.Matches(emailPattern, "test@example.org");

            // ✅ Correct: Use Assert.DoesNotMatch for negative regex tests  
            Assert.DoesNotMatch(emailPattern, "invalid.email");
            Assert.DoesNotMatch(emailPattern, "user@");
            Assert.DoesNotMatch(emailPattern, "@domain.com");
        }

        [Theory]
        [InlineData("user@domain.com", true)]
        [InlineData("test@example.org", true)]
        [InlineData("invalid.email", false)]
        [InlineData("user@", false)]
        [InlineData("@domain.com", false)]
        public void EmailPattern_ShouldMatchValidEmails(string input, bool shouldMatch)
        {
            var emailPattern = FluentRegexBuilder.StartPattern()
                .JustLetters().OneOrMore()
                .Lit("@")
                .JustLetters().OneOrMore()
                .Lit(".")
                .JustLetters().Repeat(2, 4)
                .ReturnAsRegex();

            if (shouldMatch)
            {
                // ✅ Correct: Use Assert.Matches for expected matches
                Assert.Matches(emailPattern, input);
            }
            else
            {
                // ✅ Correct: Use Assert.DoesNotMatch for expected non-matches
                Assert.DoesNotMatch(emailPattern, input);
            }
        }

        [Fact]
        public void MultiplePatterns_ShouldUseCorrectAssertions()
        {
            // Phone pattern: (XX) XXXX-XXXX
            var phoneRegex = FluentRegexBuilder.StartPattern()
                .Lit("(").JustNumbers().Repeat(2).Lit(")")
                .WhiteSpace()
                .JustNumbers().Repeat(4)
                .Lit("-")
                .JustNumbers().Repeat(4)
                .ReturnAsRegex();

            // Date pattern: DD/MM/YYYY
            var dateRegex = FluentRegexBuilder.StartPattern()
                .StartOfLine()
                .JustNumbers().Repeat(2)
                .Lit("/")
                .JustNumbers().Repeat(2)
                .Lit("/")
                .JustNumbers().Repeat(4)
                .EndOfLine()
                .ReturnAsRegex();

            // ✅ Phone validations
            Assert.Matches(phoneRegex, "(41) 9999-1234");
            Assert.Matches(phoneRegex, "(11) 5555-0000");
            Assert.DoesNotMatch(phoneRegex, "41 9999-1234");
            Assert.DoesNotMatch(phoneRegex, "(41) 999-1234");

            // ✅ Date validations
            Assert.Matches(dateRegex, "31/12/2023");
            Assert.Matches(dateRegex, "01/01/2024");
            Assert.DoesNotMatch(dateRegex, "2023/12/31");
            Assert.DoesNotMatch(dateRegex, "31-12-2023");
        }

        [Fact]
        public void SearchValuesComponent_ShouldGenerateCorrectPattern()
        {
            // This test demonstrates .NET 9 SearchValues integration
            var vowelsComponent = RegexWrap.Factories.RegexComponentFactory
                .CreateSearchValues("aeiou".AsSpan());
            
            // Test that the component generates the correct regex pattern
            var pattern = vowelsComponent.BuildPattern();
            Assert.Contains("[aeiou]", pattern);
            
            // Test SearchValues functionality directly
            Assert.True(vowelsComponent.ContainsAny("hello".AsSpan()));
            Assert.False(vowelsComponent.ContainsAny("xyz".AsSpan()));
        }

        // Helper method to demonstrate advanced pattern building
        private static Regex CreateAdvancedEmailPattern()
        {
            return FluentRegexBuilder.StartPattern()
                .StartOfLine()
                .Group(g => g.JustLetters()
                    .Or()
                    .JustNumbers()
                    .Or()
                    .Lit(".")
                    .Or()
                    .Lit("_")
                    .Or()
                    .Lit("-"))
                .OneOrMore()
                .Lit("@")
                .Group(g => g.JustLetters()
                    .Or()
                    .JustNumbers()
                    .Or()
                    .Lit("-"))
                .OneOrMore()
                .Lit(".")
                .JustLetters().Repeat(2, 6)
                .EndOfLine()
                .ReturnAsRegex();
        }
    }
}