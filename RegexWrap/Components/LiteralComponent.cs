using RegexWrap.Core;
using System.Text.RegularExpressions;

namespace RegexWrap.Components
{
    public class LiteralComponent : BaseRegexComponent
    {
        private readonly string _literal;

        public LiteralComponent(string literal) : base(RegexComponentType.Literal)
        {
            _literal = literal ?? throw new ArgumentNullException(nameof(literal));
        }

        public override string BuildPattern()
        {
            return Regex.Escape(_literal);
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_literal))
                throw new InvalidOperationException("Literal cannot be null or empty");
        }
    }
}