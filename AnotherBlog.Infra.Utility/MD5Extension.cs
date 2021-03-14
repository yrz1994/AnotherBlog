using System.Security.Cryptography;
using System.Text;

namespace AnotherBlog.Infra.Utility
{
    public static class MD5Extension
    {
        public static string GetMd5Hash(this string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
