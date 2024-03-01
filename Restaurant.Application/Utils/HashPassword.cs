using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Utils
{
    public static class HashPassword
    {
        public static string HashWithSHA256(string input)
        {
            using SHA256 sha256Hash = SHA256.Create();

            var inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] bytes = sha256Hash.ComputeHash(inputBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
