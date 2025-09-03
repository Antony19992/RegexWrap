using System.Text;

namespace RegexWrap.Core
{
    public interface IRegexComponent
    {
        string BuildPattern();
        void Validate();
        bool CanCombineWith(IRegexComponent other);
    }
}