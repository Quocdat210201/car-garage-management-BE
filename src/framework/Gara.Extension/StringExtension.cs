namespace Gara.Extension
{
    public static class StringExtension
    {
        public static string RemoveAllWhiteSpaces(this string value)
        {
            return value.Trim().Replace(" ", string.Empty);
        }
    }
}
