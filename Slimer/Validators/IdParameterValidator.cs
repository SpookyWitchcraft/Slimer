using FluentValidation;

namespace Slimer.Validators
{
    public class IdParameterValidator : AbstractValidator<int>
    {
        public IdParameterValidator()
        {
            RuleFor(x => x).InclusiveBetween(1, int.MaxValue);
        }
    }
}
