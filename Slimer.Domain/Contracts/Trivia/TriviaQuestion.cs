namespace Slimer.Domain.Contracts.Trivia
{
    public record TriviaQuestion(int Id, string Question, string Answer, string Category, bool IsEnabled, DateTime CreatedDate, DateTime UpdatedDate);
}
