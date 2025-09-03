using RegexWrap.Factories;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexWrap.Core
{
    public class PatternBuilder : IPatternBuilder
    {
        private readonly List<IRegexComponent> _components;
        private readonly IRegexFactory _regexFactory;
        private readonly StringBuilder _cachedPattern;

        public PatternBuilder(IRegexFactory? regexFactory = null)
        {
            _components = new List<IRegexComponent>();
            _regexFactory = regexFactory ?? new DefaultRegexFactory();
            _cachedPattern = new StringBuilder(capacity: 128); // Pre-allocate for better performance
        }

        public IPatternBuilder AddComponent(IRegexComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            component.Validate();
            
            if (_components.Any() && !_components.Last().CanCombineWith(component))
                throw new InvalidOperationException($"Cannot combine {_components.Last().GetType().Name} with {component.GetType().Name}");

            _components.Add(component);
            return this;
        }

        public string BuildPattern()
        {
            if (_components.Count == 0)
                return string.Empty;

            _cachedPattern.Clear();
            
            // .NET 9 optimization: Use CollectionsMarshal for better performance with List<T>
            var span = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(_components);
            foreach (ref readonly var component in span)
            {
                _cachedPattern.Append(component.BuildPattern());
            }

            return _cachedPattern.ToString();
        }

        public Regex BuildRegex(RegexOptions options = RegexOptions.None)
        {
            var pattern = BuildPattern();
            return _regexFactory.CreateRegex(pattern, options);
        }

        public void Clear()
        {
            _components.Clear();
        }
    }
}