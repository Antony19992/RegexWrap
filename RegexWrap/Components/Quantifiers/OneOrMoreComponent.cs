using RegexWrap.Core;

namespace RegexWrap.Components.Quantifiers
{
    public class OneOrMoreComponent : BaseRegexComponent
    {
        public OneOrMoreComponent() : base(RegexComponentType.Quantifier)
        {
        }

        public override string BuildPattern()
        {
            return "+";
        }
    }
}