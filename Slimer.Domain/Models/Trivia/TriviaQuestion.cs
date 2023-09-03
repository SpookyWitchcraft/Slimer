namespace Slimer.Domain.Models.Trivia
{
    public class TriviaQuestion
    {
        public int Id { get; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string Category { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime CreatedDate { get; }

        public DateTime UpdatedDate { get; }

        public TriviaQuestion(int id, string question, string answer, string category, bool isEnabled, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            Question = question;
            Answer = answer;
            Category = category;
            IsEnabled = isEnabled;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }

        public TriviaQuestion Update(string question, string answer, string category, bool isEnabled)
        {
            //rules for updating go here
            return new TriviaQuestion(Id, question, answer, category, isEnabled, CreatedDate, UpdatedDate);
        }
    }
}
