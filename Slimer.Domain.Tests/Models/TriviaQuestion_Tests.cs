using Slimer.Domain.Models.Trivia;
using System;
using Xunit;

namespace Slimer.Domain.Tests.Models
{
    public class TriviaQuestion_Tests
    {
        [Fact]
        public void Update_ShouldHaveCorrectValues()
        {
            var tq = new TriviaQuestion(1, "who?", "them", "general", true, DateTime.Now, DateTime.Now);

            var updated = tq.Update("what?", "that", "sports", false);

            Assert.Equal(tq.Id, updated.Id);
            Assert.NotEqual(tq.Question, updated.Question);
            Assert.NotEqual(tq.Answer, updated.Answer);
            Assert.NotEqual(tq.Category, updated.Category);
            Assert.NotEqual(tq.IsEnabled, updated.IsEnabled);
            Assert.Equal(tq.CreatedDate, updated.CreatedDate);
            Assert.Equal(tq.UpdatedDate, updated.UpdatedDate);
        }
    }
}
