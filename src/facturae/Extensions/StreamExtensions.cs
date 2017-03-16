// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace System.IO
{
    public static class StreamExtensions
    {
        internal static byte[] ReadBytes(this Stream stream, int count)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (count == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] buffer = new byte[count];

            stream.Read(buffer, 0, count);

            return buffer;
        }

        internal static byte[] PeekBytes(this Stream stream, int count)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (count == 0)
            {
                return Array.Empty<byte>();
            }

            var currentPosition = stream.Position;
            var buffer          = stream.ReadBytes(count);

            stream.Position = currentPosition;

            return buffer;
        }

        internal static byte PeekByte(this Stream stream)
        {
            Debug.Assert(stream != null);

            var currentPosition = stream.Position;
            var result          = stream.ReadByte();

            stream.Position = currentPosition;

            return (byte)result;
        }

        internal static void SkipBytes(this Stream stream, int offset)
        {
            stream.Seek(offset, SeekOrigin.Current);
        }
    }
}
