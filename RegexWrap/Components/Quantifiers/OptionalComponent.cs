using RegexWrap.Core;

namespace RegexWrap.Components.Quantifiers
{
    public class OptionalComponent : BaseRegexComponent
    {
        public OptionalComponent() : base(RegexComponentType.Quantifier)
        {
        }

        public override string BuildPattern()
        {
            return "?";
        }
    }
}