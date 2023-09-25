using FluentValidation;

namespace Slimer.Validators
{
    public class QueryParameterValidator : AbstractValidator<string>
    {
        public QueryParameterValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().Length(1, 255);
        }
    }
}
