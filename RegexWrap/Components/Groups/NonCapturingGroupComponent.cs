using RegexWrap.Core;

namespace RegexWrap.Components.Groups
{
    public class NonCapturingGroupComponent : BaseRegexComponent
    {
        private readonly List<IRegexComponent> _components;

        public NonCapturingGroupComponent() : base(RegexComponentType.Group)
        {
            _components = new List<IRegexComponent>();
        }

        public NonCapturingGroupComponent AddComponent(IRegexComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            _components.Add(component);
            return this;
        }

        public override string BuildPattern()
        {
            var innerPattern = string.Join("", _components.Select(c => c.BuildPattern()));
            return $"(?:{innerPattern})";
        }

        public override void Validate()
        {
            if (!_components.Any())
                throw new InvalidOperationException("Non-capturing group cannot be empty");

            foreach (var component in _components)
            {
                component.Validate();
            }
        }
    }
}