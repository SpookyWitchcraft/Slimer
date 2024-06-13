namespace Slimer.Features.ChatGpt.Extensions
{
    public static class StringExtensions
    {
        public static IList<string> ChunkWords(this string text)
        {
            var cleaned = text.Replace("\n", "");

            var output = new List<string>();
            var temp = "";

            var split = cleaned.Split(' ');

            for (var i = 0; i < split.Length; i++)
            {
                temp = $"{temp.Trim()} {split[i]}";

                if (temp.Length >= 128)
                {
                    output.Add(temp);

                    temp = "";
                }
            }

            if (!string.IsNullOrEmpty(temp))
            {
                output.Add(temp);
            }

            return output;
        }
    }
}
