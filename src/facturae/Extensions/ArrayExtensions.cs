// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// Extension methods
    /// </summary>
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Computes the SHA1 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        internal static byte[] ComputeSHA1Hash(this byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }

            using (var hashAlgorithm = SHA1.Create())
            {
                hashAlgorithm.TransformFinalBlock(buffer, 0, buffer.Length);

                return hashAlgorithm.Hash;
            }
        }

        /// <summary>
        /// Converts the given buffer to a hexadecimal string
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this ReadOnlySpan<byte> buffer) => ByteArrayToHex(buffer, string.Empty);

        /// <summary>
        /// Converts the given buffer to a hexadecimal string using the given character as separator.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this ReadOnlySpan<byte> buffer, string separator)
        {
            return ByteArrayToHex(buffer, separator);
        }

        /// <summary>
        /// Converts the given buffer to a hexadecimal string.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this ReadOnlySpan<byte> buffer, int offset, int count, string separator)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException();
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            var result = new StringBuilder(buffer.Length * 2);
            var aseps  = !string.IsNullOrEmpty(separator);

            for (int i = offset; i < count; i++)
            {
                if (aseps && result.Length > 0)
                {
                    result.Append(separator);
                }

                result.Append(buffer[i].ToString("x2"));
            }

            return result.ToString();
        }
    }
}
