// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Cryptography;

namespace System.IO;

internal static class StreamExtensions
{
    internal static byte[] ComputeSHA1Hash(this Stream stream, bool leavOpen = false)
    {
        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        using var hashAlgorithm = SHA1.Create();

        var hash = hashAlgorithm.ComputeHash(stream);

        if (!leavOpen)
        {
            stream.Dispose();
        }

        return hash;
    }
}
