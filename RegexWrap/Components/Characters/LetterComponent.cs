using RegexWrap.Core;

namespace RegexWrap.Components.Characters
{
    public class LetterComponent : BaseRegexComponent
    {
        public LetterComponent() : base(RegexComponentType.Character)
        {
        }

        public override string BuildPattern()
        {
            return @"[a-zA-Z]";
        }
    }
}