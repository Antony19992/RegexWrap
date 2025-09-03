using RegexWrap.Core;

namespace RegexWrap.Components.Characters
{
    public class AnyCharComponent : BaseRegexComponent
    {
        public AnyCharComponent() : base(RegexComponentType.Character)
        {
        }

        public override string BuildPattern()
        {
            return ".";
        }
    }
}