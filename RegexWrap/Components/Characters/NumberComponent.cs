using RegexWrap.Core;

namespace RegexWrap.Components.Characters
{
    public class NumberComponent : BaseRegexComponent
    {
        public NumberComponent() : base(RegexComponentType.Character)
        {
        }

        public override string BuildPattern()
        {
            return @"\d";
        }
    }
}