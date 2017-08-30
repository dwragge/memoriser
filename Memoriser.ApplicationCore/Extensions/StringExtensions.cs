using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        public static string JoinPaths(this string basePart, string relativePart)
        {
            var slashSeparators = new[] { '/', '\\' };
            basePart = basePart.TrimEnd(slashSeparators);
            relativePart = relativePart.TrimStart(slashSeparators);
            return $"{basePart}/{relativePart}".TrimEnd(slashSeparators);
        }

        public static bool IsOnlyLetterCharacters(this string str)
        {
            const string pattern = @"^[\p{L} | \s]*$";
            return Regex.IsMatch(str, pattern);
        }
    }
}
