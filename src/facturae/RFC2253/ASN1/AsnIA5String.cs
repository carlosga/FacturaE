// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace ASN1;

internal sealed class AsnIA5String : AsnString
{
    public AsnIA5String(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
        : base(id, buffer, Encoding.ASCII.GetString(buffer.Span))
    {
    }
}
