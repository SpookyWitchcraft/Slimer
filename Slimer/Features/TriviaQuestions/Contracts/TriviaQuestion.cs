using Microsoft.EntityFrameworkCore;

namespace Slimer.Features.TriviaQuestions.Contracts
{
    [PrimaryKey(nameof(TriviaQuestionId))]
    public class TriviaQuestion
    {
        public int TriviaQuestionId { get; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string Category { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
