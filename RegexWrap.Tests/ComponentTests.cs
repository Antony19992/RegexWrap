using RegexWrap.Components.Characters;
using RegexWrap.Components.Quantifiers;
using RegexWrap.Components.Anchors;
using RegexWrap.Factories;
using Xunit;

namespace RegexWrap.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void NumberComponent_ShouldGenerateCorrectPattern()
        {
            var component = new NumberComponent();
            Assert.Equal(@"\d", component.BuildPattern());
        }

        [Fact]
        public void LetterComponent_ShouldGenerateCorrectPattern()
        {
            var component = new LetterComponent();
            Assert.Equal(@"[a-zA-Z]", component.BuildPattern());
        }

        [Fact]
        public void RepeatComponent_ExactCount_ShouldGenerateCorrectPattern()
        {
            var component = new RepeatComponent(3);
            Assert.Equal("{3}", component.BuildPattern());
        }

        [Fact]
        public void RepeatComponent_Range_ShouldGenerateCorrectPattern()
        {
            var component = new RepeatComponent(2, 5);
            Assert.Equal("{2,5}", component.BuildPattern());
        }

        [Fact]
        public void Factory_ShouldCreateValidComponents()
        {
            var number = RegexComponentFactory.CreateNumber();
            var letter = RegexComponentFactory.CreateLetter();
            var repeat = RegexComponentFactory.CreateRepeat(3);

            Assert.NotNull(number);
            Assert.NotNull(letter);
            Assert.NotNull(repeat);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        public void RepeatComponent_NegativeCount_ShouldThrowException(int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RepeatComponent(count));
        }
    }
}