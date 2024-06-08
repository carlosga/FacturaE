// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Cryptography;

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

        return SHA1.HashData(buffer.AsSpan());
    }

    /// <summary>
    /// Converts the given buffer to a hexadecimal string using the given character as separator.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="separator">The separator.</param>
    /// <returns></returns>
    internal static string ByteArrayToHex(this ReadOnlySpan<byte> buffer, string separator)
    {
        var result = new StringBuilder(buffer.Length * 2);
        var aseps  = !string.IsNullOrEmpty(separator);

        for (var i = 0; i < buffer.Length; i++)
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
