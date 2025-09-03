using RegexWrap.Core;
using System.Text;

namespace RegexWrap.Components.Groups
{
    public class GroupComponent : BaseRegexComponent
    {
        private readonly List<IRegexComponent> _components;
        private readonly string? _name;

        public GroupComponent(string? name = null) : base(RegexComponentType.Group)
        {
            _components = new List<IRegexComponent>();
            _name = name;
        }

        public GroupComponent AddComponent(IRegexComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            _components.Add(component);
            return this;
        }

        public override string BuildPattern()
        {
            var innerPattern = string.Join("", _components.Select(c => c.BuildPattern()));
            
            return string.IsNullOrEmpty(_name) 
                ? $"({innerPattern})"
                : $"(?<{_name}>{innerPattern})";
        }

        public override void Validate()
        {
            if (!_components.Any())
                throw new InvalidOperationException("Group cannot be empty");

            foreach (var component in _components)
            {
                component.Validate();
            }

            if (!string.IsNullOrEmpty(_name) && !IsValidGroupName(_name))
                throw new InvalidOperationException($"Invalid group name: {_name}");
        }

        private static bool IsValidGroupName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && 
                   name.All(c => char.IsLetterOrDigit(c) || c == '_') &&
                   char.IsLetter(name[0]);
        }
    }
}