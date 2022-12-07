using System;
using System.Security.Cryptography;
using System.Text;

namespace Caliph.Library.Helper
{
    /// <summary>
    /// Generate Md5 and SHA hashes in C#.NET.
    /// Code Link Ref: https://gist.github.com/rmacfie/828054
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Calculates the MD5 hash for the given string.
        /// </summary>
        /// <returns>A 32 char long MD5 hash.</returns>
        public static string GetHashMd5(this string input)
        {
            return ComputeHash(input, new MD5CryptoServiceProvider());
        }

        /// <summary>
        /// Calculates the SHA-1 hash for the given string.
        /// </summary>
        /// <returns>A 40 char long SHA-1 hash.</returns>
        public static string GetHashSha1(this string input)
        {
            return ComputeHash(input, new SHA1Managed());
        }

        /// <summary>
        /// Calculates the SHA-256 hash for the given string.
        /// </summary>
        /// <returns>A 64 char long SHA-256 hash.</returns>
        public static string GetHashSha256(this string input)
        {
            return ComputeHash(input, new SHA256Managed());
        }

        /// <summary>
        /// Calculates the SHA-384 hash for the given string.
        /// </summary>
        /// <returns>A 96 char long SHA-384 hash.</returns>
        public static string GetHashSha384(this string input)
        {
            return ComputeHash(input, new SHA384Managed());
        }

        /// <summary>
        /// Calculates the SHA-512 hash for the given string.
        /// </summary>
        /// <returns>A 128 char long SHA-512 hash.</returns>
        public static string GetHashSha512(this string input)
        {
            return ComputeHash(input, new SHA512Managed());
        }

        /// <summary>
        /// Compute Hash
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hashProvider"></param>
        /// <returns></returns>
        private static string ComputeHash(string input, HashAlgorithm hashProvider)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (hashProvider == null)
            {
                throw new ArgumentNullException("hashProvider");
            }

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = hashProvider.ComputeHash(inputBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            return hash;
        }

        /// <summary>
        /// Decrypt password
        /// </summary>
        /// <param name="pwd">Encrypted Password</param>
        /// <returns></returns>
        public static string DecryptPwd(string encryptedPwd, string secretKey)
        {
            try
            {
                // Create sha256 hash
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(secretKey));

                // Create secret IV
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                AESHelper oAESHelper = new AESHelper();
                string decryptedPwd = oAESHelper.AES256DecryptString(encryptedPwd, key, iv);

                return decryptedPwd;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <returns></returns>
        public static string EncryptPwd(string value, string secretKey)
        {
            try
            {
                // Create sha256 hash
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(secretKey));

                // Create secret IV
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                AESHelper oAESHelper = new AESHelper();
                string encryptedValue = oAESHelper.AES256EncryptString(value, key, iv);

                return encryptedValue;
            }
            catch (Exception)
            {
                //return ex.ToString();
                return "";
            }
        }

        public static string EncryptText(string value, string secretKey)
        {
            try
            {
                // Create sha256 hash
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(secretKey));

                // Create secret IV
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                AESHelper oAESHelper = new AESHelper();
                string encryptedValue = oAESHelper.AES256EncryptString(value, key, iv);

                return encryptedValue;
            }
            catch (Exception)
            {
                //return ex.ToString();
                return "";
            }
        }

        public static string DecryptText(string encryptedText, string secretKey)
        {
            try
            {
                // Create sha256 hash
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(secretKey));

                // Create secret IV
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                AESHelper oAESHelper = new AESHelper();
                string decryptedPwd = oAESHelper.AES256DecryptString(encryptedText, key, iv);

                return decryptedPwd;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
