// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

internal sealed class AsnOctetString : AsnString
{
    public AsnOctetString(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
        : base(id, buffer, buffer.Span.ByteArrayToHex(" "))
    {
    }
}