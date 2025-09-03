using RegexWrap.Core;

namespace RegexWrap.Components.Anchors
{
    public class StartOfLineComponent : BaseRegexComponent
    {
        public StartOfLineComponent() : base(RegexComponentType.Anchor)
        {
        }

        public override string BuildPattern()
        {
            return "^";
        }

        public override bool CanCombineWith(IRegexComponent other)
        {
            return other is not StartOfLineComponent;
        }
    }
}