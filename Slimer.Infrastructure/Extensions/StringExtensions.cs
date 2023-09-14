namespace Slimer.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static IList<string> ChunkWords(this string text)
        {
            var output = new List<string>();
            var temp = "";

            var split = text.Split(' ');

            for (var i = 0; i < split.Length; i++)
            {
                temp = $"{temp.Trim()} {split[i]}";

                if (temp.Length >= 128)
                {
                    output.Add(temp);

                    temp = "";
                }
            }

            if(!string.IsNullOrEmpty(temp))
            {
                output.Add(temp);
            }

            return output;
        }
    }
}
