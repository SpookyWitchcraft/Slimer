using FluentValidation;
using Slimer.Domain.Models.Trivia;

namespace Slimer.Validators
{
    public class TriviaQuestionValidator : AbstractValidator<TriviaQuestion>
    {
        public TriviaQuestionValidator()
        {
            RuleFor(x => x.Answer).NotNull().NotEmpty().Length(1, 5000);
            RuleFor(x => x.Question).NotNull().NotEmpty().Length(1, 5000);
            RuleFor(x => x.Category).NotNull().NotEmpty().Length(1, 255);
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.CreatedDate).NotEqual(default(DateTime));
            RuleFor(x => x.UpdatedDate).NotEqual(default(DateTime));
        }
    }
}
