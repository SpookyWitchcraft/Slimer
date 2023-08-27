using CsvHelper;
using Slimer.Services.Interfaces;
using System.Globalization;

namespace Slimer.Services
{
    public class TriviaService : ITriviaService
    {
        private readonly TriviaQuestion[] _questions;

        public TriviaService()
        {
            using var reader = new StreamReader(@"c:\temp\trivia.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            _questions = csv.GetRecords<TriviaQuestion>().ToArray();
        }

        public TriviaQuestion GetQuestion()
        {
            var rand = new Random();

            var next = rand.Next(0, _questions.Length);

            return _questions[next];
        }
    }
}
