using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
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

        public static string ReduceWhitespace(this string value)
        {
            var newString = new StringBuilder();
            bool previousIsWhitespace = false;
            foreach (char t in value)
            {
                if (char.IsWhiteSpace(t))
                {
                    if (previousIsWhitespace)
                    {
                        continue;
                    }

                    previousIsWhitespace = true;
                }
                else
                {
                    previousIsWhitespace = false;
                }

                newString.Append(t);
            }

            return newString.ToString().Trim();
        }
    }
}
