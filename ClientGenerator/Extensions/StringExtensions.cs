namespace ClientGenerator.Extensions
{
    public static class StringExtensions
    {
        public static string ToKabobCase(this string @string)
        {

            IEnumerable<char> ConvertChar(char c, int index)
            {
                if (char.IsUpper(c))
                {
                    if (index != 0) yield return char.ToLower('-');
                    yield return char.ToLower(c);
                }
                else yield return c;
            }

            return string.Concat(@string.SelectMany(ConvertChar));

        }
        public static string ToCamelCase(this string @string)
        {
            if (string.IsNullOrWhiteSpace(@string))
                return string.Empty;

            var result = new List<char>();
            var hasLower = false;
            foreach (var letter in @string)
            {

                var n = (int)letter;
                if (hasLower)
                    result.Add(letter);
                else if (n >= 65 & n <= 90)
                {
                    result.Add(letter.ToString().ToLower().First());
                }
                else
                {
                    result.Add(letter);
                    hasLower = true;
                }
            }

            return string.Join(string.Empty, result.ToArray());
        }
    }
}
