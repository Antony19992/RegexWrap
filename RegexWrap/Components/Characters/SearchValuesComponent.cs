using RegexWrap.Core;
using System.Buffers;

namespace RegexWrap.Components.Characters
{
    public class SearchValuesComponent : BaseRegexComponent
    {
        private readonly SearchValues<char> _searchValues;
        private readonly string _pattern;
        private readonly bool _negated;

        public SearchValuesComponent(ReadOnlySpan<char> allowedChars, bool negated = false) 
            : base(RegexComponentType.Character)
        {
            _searchValues = SearchValues.Create(allowedChars);
            _negated = negated;
            
            // Build the regex pattern from the search values
            var chars = string.Create(allowedChars.Length, allowedChars, (span, source) =>
            {
                source.CopyTo(span);
            });
            
            _pattern = _negated ? $"[^{EscapeForCharacterClass(chars)}]" : $"[{EscapeForCharacterClass(chars)}]";
        }

        public override string BuildPattern()
        {
            return _pattern;
        }

        public bool ContainsAny(ReadOnlySpan<char> text)
        {
            return text.ContainsAny(_searchValues);
        }

        private static string EscapeForCharacterClass(string input)
        {
            return input.Replace("]", @"\]")
                       .Replace("^", @"\^")
                       .Replace("-", @"\-")
                       .Replace(@"\", @"\\");
        }
    }
}