using System.Text.RegularExpressions;

namespace JobFinder.Helpers
{
    public static class ExtensionMethods
    {
        public static string CleanStr (this string input)
        {
            input = input.Trim().Replace("&zwnj;", " ");
            return Regex.Replace(input, @"\t|\n|\r", "");
        }
    }
}