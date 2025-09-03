using RegexWrap.Core;

namespace RegexWrap.Components
{
    public abstract class BaseRegexComponent : IRegexComponent
    {
        protected readonly RegexComponentType ComponentType;

        protected BaseRegexComponent(RegexComponentType componentType)
        {
            ComponentType = componentType;
        }

        public abstract string BuildPattern();

        public virtual void Validate()
        {
        }

        public virtual bool CanCombineWith(IRegexComponent other)
        {
            return other != null;
        }
    }
}