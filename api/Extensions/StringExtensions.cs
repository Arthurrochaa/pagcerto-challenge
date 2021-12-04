using System.Text.RegularExpressions;

namespace api.Extensions
{
    public static class StringExtensions
    {
        public static string FirstFourCharacters(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 4)
                str = str[..4];

            return str;
        }

        public static string LastFourCharacters(this string str)
        {
            if(!string.IsNullOrEmpty(str) && str.Length > 4)
                str = str[^4..];

            return str;
        }

        public static string OnlyNumbers(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var regex = new Regex(@"^\d$");
                return regex.Replace(str, string.Empty).Trim();
            }

            return str;
        }
    }
}
