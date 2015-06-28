using System.Linq;

namespace TaskB.Libs
{
    public static class StringHelper
    {
        public static string ToUpperFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string CombineString(this string first, string second, string sparator)
        {
            if (!string.IsNullOrWhiteSpace(first) && !string.IsNullOrWhiteSpace(second))
            {
                return string.Format("{0}{1}{2}", first, sparator, second);
            }

            if (!string.IsNullOrWhiteSpace(first) && string.IsNullOrWhiteSpace(second))
            {
                return first;
            }

            if (string.IsNullOrWhiteSpace(first) && !string.IsNullOrWhiteSpace(second))
            {
                return second;
            }

            return string.Empty;
        }
    }
}