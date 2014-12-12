/* FacturaE - The MIT License (MIT)
 * 
 * Copyright (c) 2012-2014 Carlos Guzmán Álvarez
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Security.Cryptography;
using System.Text;

namespace System
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
            if (buffer == null)
            {
                return null;
            }

            using (MD5 md5 = MD5.Create())
            {
                md5.TransformFinalBlock(buffer, 0, buffer.Length);

                return md5.Hash;
            }
        }

        /// <summary>
        /// Computes the SHA1 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }

            using (SHA1 hashAlgorithm = SHA1.Create())
            {
                hashAlgorithm.TransformFinalBlock(buffer, 0, buffer.Length);

                return hashAlgorithm.Hash;
            }
        }

        /// <summary>
        /// Computes the MD5 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeMD5Hash(this string[] values)
        {
            if (values == null)
            {
                return null;
            }

            using (MD5 hashAlgorithm = MD5.Create())
            {
                foreach (string value in values)
                {
                    if (value != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        byte[] output = new byte[buffer.Length];
                        
                        hashAlgorithm.TransformBlock(buffer, 0, buffer.Length, output, 0);
                    }
                }

                hashAlgorithm.TransformFinalBlock(new byte[0], 0, 0);

                return hashAlgorithm.Hash;
            }
        }

        /// <summary>
        /// Computes the SHA1 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this string[] values)
        {
            if (values == null)
            {
                return null;
            }

            using (SHA1 hashAlgorithm = SHA1.Create())
            {
                foreach (string value in values)
                {
                    if (value != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        byte[] output = new byte[buffer.Length];

                        hashAlgorithm.TransformBlock(buffer, 0, buffer.Length, output, 0);
                    }
                }

                hashAlgorithm.TransformFinalBlock(new byte[0], 0, 0);

                return hashAlgorithm.Hash;
            }
        }

        /// <summary>
        /// Convert a byte array to an hex string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }

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
