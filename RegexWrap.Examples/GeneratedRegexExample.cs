using System.Text.RegularExpressions;

namespace RegexWrap.Examples
{
    public static partial class GeneratedRegexExample
    {
        // Direct usage of .NET 9's native GeneratedRegex (works without our custom source generator)
        [GeneratedRegex(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex EmailPattern();

        [GeneratedRegex(@"^[a-zA-Z]+\s\d{2,4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        public static partial Regex NameAndNumberPattern();

        [GeneratedRegex(@"^\d{2}/\d{2}/\d{4}$", RegexOptions.Compiled)]
        public static partial Regex DatePattern();

        public static void RunExamples()
        {
            Console.WriteLine("=== Generated Regex Examples (.NET 9) ===\n");

            // Test native .NET 9 generated regex patterns
            var nameRegex = NameAndNumberPattern();
            Console.WriteLine($"NameAndNumber: 'Test 123' -> {nameRegex.IsMatch("Test 123")}");
            Console.WriteLine($"NameAndNumber: '123 Test' -> {nameRegex.IsMatch("123 Test")}");

            var dateRegex = DatePattern();
            Console.WriteLine($"Date: '31/12/2023' -> {dateRegex.IsMatch("31/12/2023")}");
            Console.WriteLine($"Date: '2023-12-31' -> {dateRegex.IsMatch("2023-12-31")}");

            var emailRegex = EmailPattern();
            Console.WriteLine($"Email: 'user@domain.com' -> {emailRegex.IsMatch("user@domain.com")}");
            Console.WriteLine($"Email: 'invalid.email' -> {emailRegex.IsMatch("invalid.email")}");

            Console.WriteLine("\n=== Performance Benefits ===");
            Console.WriteLine("Generated regexes are AOT-compiled and optimized at build time!");
            Console.WriteLine("No runtime compilation overhead - maximum performance for .NET 9!");
            
            Console.WriteLine("\n=== RegexWrap Integration ===");
            Console.WriteLine("You can build patterns with RegexWrap and then use GeneratedRegex for production:");
            var pattern = FluentRegexBuilder.StartPattern()
                .StartOfLine()
                .JustLetters().OneOrMore()
                .Lit("@")
                .JustLetters().OneOrMore()
                .ReturnAsString();
            Console.WriteLine($"Generated pattern: {pattern}");
        }
    }
}