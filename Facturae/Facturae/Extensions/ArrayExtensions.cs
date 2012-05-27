using System;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicInvoice.Extensions
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ArrayExtensions
    {
        #region · Extensions ·

        /// <summary>
        /// Converts a given byte array to a base-64 string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToBase64String(this byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Computes the MD5 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeMD5Hash(this byte[] buffer)
        {
            MD5 md5 = MD5.Create();
            md5.TransformFinalBlock(buffer, 0, buffer.Length);

            return md5.Hash;
        }

        /// <summary>
        /// Computes the SHA1 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this byte[] buffer)
        {
            SHA1 hashAlgorithm = SHA1.Create();
            hashAlgorithm.TransformFinalBlock(buffer, 0, buffer.Length);

            return hashAlgorithm.Hash;
        }

        /// <summary>
        /// Computes the MD5 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeMD5Hash(this string[] values)
        {
            MD5 hashAlgorithm = MD5.Create();

            foreach (string value in values)
            {
                if (value != null)
                {
                    byte[]  buffer  = Encoding.UTF8.GetBytes(value);
                    byte[]  output  = new byte[buffer.Length];
                    int     count   = hashAlgorithm.TransformBlock(buffer, 0, buffer.Length, output, 0);
                }
            }

            hashAlgorithm.TransformFinalBlock(new byte[0], 0, 0);

            return hashAlgorithm.Hash;
        }

        /// <summary>
        /// Computes the SHA1 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static byte[] ComputeSHA1Hash(this string[] values)
        {
            SHA1 hashAlgorithm = SHA1.Create();

            foreach (string value in values)
            {
                if (value != null)
                {
                    byte[]  buffer  = Encoding.UTF8.GetBytes(value);
                    byte[]  output  = new byte[buffer.Length];
                    int     count   = hashAlgorithm.TransformBlock(buffer, 0, buffer.Length, output, 0);
                }
            }

            hashAlgorithm.TransformFinalBlock(new byte[0], 0, 0);

            return hashAlgorithm.Hash;
        }

        /// <summary>
        /// Convert a byte array to an hex string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] buffer)
        {
            StringBuilder hex = new StringBuilder();

            for (int i = 0; i < buffer.Length; i++)
            {
                hex.Append(buffer[i].ToString("x2"));
            }

            return hex.ToString();
        }

        #endregion
    }
}
