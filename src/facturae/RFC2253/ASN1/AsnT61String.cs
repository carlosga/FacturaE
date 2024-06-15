// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

internal sealed class AsnT61String : AsnString
{
    public AsnT61String(AsnIdentifier id, ReadOnlyMemory<byte> buffer) : base(id, buffer, string.Empty)
    {
        throw new NotImplementedException();
    }
}