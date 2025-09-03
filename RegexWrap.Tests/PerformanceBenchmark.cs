using System.Diagnostics;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace RegexWrap.Tests
{
    public class PerformanceBenchmark
    {
        private readonly ITestOutputHelper _output;

        public PerformanceBenchmark(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ComparePerformance_FluentVsLegacy()
        {
            const int iterations = 100000;
            const string testInput = "Test123";

            // Warm up
            for (int i = 0; i < 1000; i++)
            {
                CreateFluentRegex();
                CreateLegacyRegex();
            }

            // Benchmark FluentRegexBuilder
            var fluentStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                var regex = CreateFluentRegex();
                regex.IsMatch(testInput);
            }
            fluentStopwatch.Stop();

            // Benchmark Legacy RegexBuilder
            var legacyStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                #pragma warning disable CS0618
                var regex = CreateLegacyRegex();
                #pragma warning restore CS0618
                regex.IsMatch(testInput);
            }
            legacyStopwatch.Stop();

            _output.WriteLine($"FluentRegexBuilder: {fluentStopwatch.ElapsedMilliseconds}ms");
            _output.WriteLine($"Legacy RegexBuilder: {legacyStopwatch.ElapsedMilliseconds}ms");
            
            // The new implementation should not be significantly slower
            Assert.True(fluentStopwatch.ElapsedMilliseconds <= legacyStopwatch.ElapsedMilliseconds * 2,
                "FluentRegexBuilder should not be more than 2x slower than legacy implementation");
        }

        private static Regex CreateFluentRegex()
        {
            return FluentRegexBuilder.StartPattern()
                .JustLetters().OneOrMore()
                .JustNumbers().OneOrMore()
                .ReturnAsRegex();
        }

        private static Regex CreateLegacyRegex()
        {
            return RegexBuilder.StartPattern()
                .JustLetters().OneOrMore()
                .JustNumbers().OneOrMore()
                .ReturnAsRegex();
        }
    }
}