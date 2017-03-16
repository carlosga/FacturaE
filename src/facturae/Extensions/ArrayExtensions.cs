// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ArrayExtensions
    {
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

            using (var hashAlgorithm = SHA1.Create())
            {
                hashAlgorithm.TransformFinalBlock(buffer, 0, buffer.Length);

                return hashAlgorithm.Hash;
            }
        }
    }
}
