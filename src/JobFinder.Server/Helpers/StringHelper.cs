using System.Text.RegularExpressions;

namespace JobFinder.Server.Helpers;

public static class StringHelper
{
    public static string CleanStr(this string input)
    {
        input = input.Trim().Replace("&zwnj;", " ");
        return Regex.Replace(input, @"\t|\n|\r", "");
    }
}