using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CoinTNet.Common
{
    /// <summary>
    /// Helper for general security
    /// </summary>
    class SecurityHelper
    {
        private static readonly byte[] Salt = { 0x12, 0x86, 0x25, 0x23, 0x61, 0x52, 0x43, 0x74, 0x10, 0x88, 0x18, 0x32, 0x46 };
        /// <summary>
        /// Encrypts a string with AES
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static string EncryptAES(string clearText, string encryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                using (var derivedBytes = new Rfc2898DeriveBytes(encryptionKey, Salt))
                {
                    encryptor.Key = derivedBytes.GetBytes(32);
                    encryptor.IV = derivedBytes.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return clearText;
        }

        /// <summary>
        /// Decrypts a AES string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static string DecryptAES(string cipherText, string encryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                using (var derivedBytes = new Rfc2898DeriveBytes(encryptionKey, Salt))
                {
                    encryptor.Key = derivedBytes.GetBytes(32);
                    encryptor.IV = derivedBytes.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Creates a SHA512 hash of a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="salt"></param>
        /// <param name="extraHashKey"></param>
        /// <returns></returns>
        public static string HashSha512(string text, string salt, string extraHashKey = "")
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text + extraHashKey + salt)));
            }
        }
        /// <summary>
        /// Generates a random salt
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            var rd = new Random();
            byte[] b = new byte[10];
            rd.NextBytes(b);
            return Convert.ToBase64String(b);

        }
    }
}
