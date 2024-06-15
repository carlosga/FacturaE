// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace ASN1;

internal sealed class AsnInteger : AsnValueObject<BigInteger>
{
    public AsnInteger(AsnIdentifier id, ReadOnlyMemory<byte> buffer) : base(id, buffer)
    {
        Value = new BigInteger(buffer.Span);
    }
}