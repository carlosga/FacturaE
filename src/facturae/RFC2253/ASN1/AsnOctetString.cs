// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace ASN1
{
    public sealed class AsnOctetString
        : AsnString
    {
        public AsnOctetString(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
            : base(id, buffer, buffer.Span.ByteArrayToHex(" "))
        {
        }
    }
}
