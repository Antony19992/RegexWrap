using RegexWrap.Core;

namespace RegexWrap.Components
{
    public class AlternationComponent : BaseRegexComponent
    {
        public AlternationComponent() : base(RegexComponentType.Alternation)
        {
        }

        public override string BuildPattern()
        {
            return "|";
        }
    }
}