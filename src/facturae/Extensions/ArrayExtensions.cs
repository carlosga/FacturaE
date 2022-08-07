// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Cryptography;
using System.Text;

namespace System;

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
        if (buffer is null)
        {
            return null;
        }

        using var hashAlgorithm = SHA1.Create();

        return hashAlgorithm.ComputeHash(buffer, 0, buffer.Length);
    }

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
}
