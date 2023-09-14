using Slimer.Infrastructure.Extensions;
using Xunit;

namespace Slimer.Infrastructure.Tests.Extensions
{
    public class StringExtension_Tests
    {
        [Fact]
        public void ChunkWords_ShouldCreateStringsWithFullWords()
        {
            //WARNING
            //Changing this string will change the results of the test
            const string preChunked = "A praying mantis lurked in the foliage, patiently awaiting its unsuspecting prey to venture too close. Ladybugs, those tiny red sentinels of the garden, patrolled the leaves, ready to pounce on any aphid that dared to feast on their precious plants.";

            var postChunked = preChunked.ChunkWords();

            var resplitFirst = postChunked[0].Split(' ');
            var resplitLast = postChunked[1].Split(' ');

            Assert.True(postChunked.Count == 2);
            Assert.True(resplitFirst[^1] == "sentinels");
            Assert.True(resplitLast[0] == "of");
            Assert.True(resplitLast[^1] == "plants.");
        }
    }
}
