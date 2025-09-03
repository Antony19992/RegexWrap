using RegexWrap.Core;

namespace RegexWrap.Components.Quantifiers
{
    public class RepeatComponent : BaseRegexComponent
    {
        private readonly int _min;
        private readonly int? _max;

        public RepeatComponent(int count) : base(RegexComponentType.Quantifier)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative");
            
            _min = count;
            _max = count;
        }

        public RepeatComponent(int min, int max) : base(RegexComponentType.Quantifier)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min), "Min must be non-negative");
            if (max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "Max must be greater than or equal to min");

            _min = min;
            _max = max;
        }

        public override string BuildPattern()
        {
            return _max.HasValue && _max != _min 
                ? $"{{{_min},{_max}}}" 
                : $"{{{_min}}}";
        }

        public override void Validate()
        {
            if (_min < 0)
                throw new InvalidOperationException("Min repetition count cannot be negative");
            
            if (_max.HasValue && _max < _min)
                throw new InvalidOperationException("Max repetition count cannot be less than min");
        }
    }
}