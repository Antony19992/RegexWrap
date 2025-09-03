using RegexWrap;
using RegexWrap.Generators;
using System.Text.RegularExpressions;

Console.WriteLine("=== RegexWrap 2.0 for .NET 9 Examples ===\n");

// Example 1: Using new FluentRegexBuilder (recommended)
Console.WriteLine("1. FluentRegexBuilder - Email validation:");
var emailRegex = FluentRegexBuilder.StartPattern()
    .StartOfLine()
    .Group(r => r.JustLetters().OneOrMore())
    .Lit("@")
    .Group(r => r.JustLetters().OneOrMore())
    .Lit(".")
    .Group(r => r.JustLetters().Repeat(2, 3))
    .EndOfLine()
    .ReturnAsRegex();

TestRegex(emailRegex, "test@email.com", "invalid-email");

// Example 2: Legacy RegexBuilder (backward compatibility)
Console.WriteLine("\n2. RegexBuilder (original API):");
var phoneRegex = RegexBuilder.StartPattern()
    .Lit("(").JustNumbers().Repeat(2).Lit(")")
    .WhiteSpace()
    .JustNumbers().Repeat(4)
    .Lit("-")
    .JustNumbers().Repeat(4)
    .ReturnAsRegex();

TestRegex(phoneRegex, "(41) 9999-1234", "invalid-phone");

// Example 3: Complex pattern with groups and alternation
Console.WriteLine("\n3. Complex pattern - Date validation (DD/MM/YYYY or DD-MM-YYYY):");
var dateRegex = FluentRegexBuilder.StartPattern()
    .StartOfLine()
    .JustNumbers().Repeat(2)
    .NonCapturingGroup(r => r.Lit("/").Or().Lit("-"))
    .JustNumbers().Repeat(2)
    .NonCapturingGroup(r => r.Lit("/").Or().Lit("-"))
    .JustNumbers().Repeat(4)
    .EndOfLine()
    .ReturnAsRegex();

TestRegex(dateRegex, "31/12/2023", "31-12-2023", "2023/12/31");

// Example 4: Using Source Generator (compile-time optimization)
Console.WriteLine("\n4. Source Generator example:");
Console.WriteLine("Note: Source generators would be used like this:");
Console.WriteLine("[RegexGenerated(@\"^[a-zA-Z]+\\s\\d{2,4}$\")]");
Console.WriteLine("public static partial Regex NameAndNumberPattern();");

static void TestRegex(Regex regex, params string[] testCases)
{
    Console.WriteLine($"Pattern: {regex}");
    foreach (var test in testCases)
    {
        var match = regex.IsMatch(test);
        Console.WriteLine($"  '{test}' -> {(match ? "✅ Match" : "❌ No match")}");
    }
}

// Example 5: Performance comparison
Console.WriteLine("\n5. Performance demonstration:");
var iterations = 10000;
var testInput = "Test123";

var fluentBuilder = FluentRegexBuilder.StartPattern()
    .JustLetters().OneOrMore()
    .JustNumbers().OneOrMore()
    .ReturnAsRegex();

var start = DateTime.Now;
for (int i = 0; i < iterations; i++)
{
    fluentBuilder.IsMatch(testInput);
}
var elapsed = DateTime.Now - start;

Console.WriteLine($"FluentRegexBuilder: {iterations:N0} operations in {elapsed.TotalMilliseconds:F2}ms");
Console.WriteLine($"Average: {(elapsed.TotalMilliseconds / iterations):F4}ms per operation");

// Example 6: .NET 9 Generated Regex Integration
Console.WriteLine("\n6. .NET 9 Generated Regex Integration:");
RegexWrap.Examples.GeneratedRegexExample.RunExamples();

Console.WriteLine("\n=== End of Examples ===");