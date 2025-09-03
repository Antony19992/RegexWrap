using RegexWrap.Core;

namespace RegexWrap.Components.Characters
{
    public class WhitespaceComponent : BaseRegexComponent
    {
        public WhitespaceComponent() : base(RegexComponentType.Character)
        {
        }

        public override string BuildPattern()
        {
            return @"\s";
        }
    }
}