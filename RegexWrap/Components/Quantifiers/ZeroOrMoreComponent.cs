using RegexWrap.Core;

namespace RegexWrap.Components.Quantifiers
{
    public class ZeroOrMoreComponent : BaseRegexComponent
    {
        public ZeroOrMoreComponent() : base(RegexComponentType.Quantifier)
        {
        }

        public override string BuildPattern()
        {
            return "*";
        }
    }
}