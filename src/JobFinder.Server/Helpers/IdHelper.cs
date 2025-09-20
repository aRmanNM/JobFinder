using System.Security.Cryptography;
using System.Text;

namespace JobFinder.Server.Helpers;

public static class IdHelper
{
    public static string GetId(string url)
    {
        return GenerateMD5Hash(url);
    }

    private static string GenerateMD5Hash(string input)
    {
        using var md5Hash = MD5.Create();

        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}