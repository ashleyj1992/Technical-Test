namespace Technical_Test.Extensions
{
    public static class StringExtensions
    {
        public static string GetFirstWord(this string value)
        {
            return GetWordByIndex(value, 0, ' ');
        }

        public static string GetWordByIndex(this string value, int index, char separator)
        {
            var values = value.Split(separator);
            return values[index];
        }
    }
}
