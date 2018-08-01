// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;

namespace ASN1
{
    /// <summary>
    /// ASN.1 BMP (Basic Multilingual Plane) String
    /// </summary>
    public sealed class AsnBmpString
        : AsnString
    {
        private static readonly Encoding s_encoding1201 = Encoding.GetEncoding(1201);
        private static readonly Encoding s_encoding1200 = Encoding.GetEncoding(1200);

        // http://www.tech-archive.net/Archive/DotNet/microsoft.public.dotnet.languages.csharp/2007-11/msg03278.html
        private static string Encode(ReadOnlyMemory<byte> buffer)
        {
            if (buffer.Span[0] == 0)
            {
                return s_encoding1201.GetString(buffer.Span);
            }
            return s_encoding1200.GetString(buffer.Span);
        }

        public AsnBmpString(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
            : base(id, buffer, Encode(buffer))
        {
        }
    }
}
