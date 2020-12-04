using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using LoginResource.Domain;

namespace LoginResource.Util
{
    public class HashUtil
    {
        private HashUtil() { }

        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        public static bool CheckPassword(User user, string passwort)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string passwordHash = HashUtil.GetHash(sha256, passwort);
                if (HashUtil.VerifyHash(sha256, passwort, user.Passwort))
                {
                    return true;
                }
            }
            return false;
        }

        public static string HashPassword(string passwort)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string passwordHash = HashUtil.GetHash(sha256, passwort);
                return passwordHash;
            }
        }
    }
}
