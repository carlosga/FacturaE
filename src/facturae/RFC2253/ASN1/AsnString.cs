// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace ASN1
{
    public abstract class AsnString
        : AsnValueObject<string>
    {
        protected AsnString(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
            : base(id, buffer)
        {
        }

        protected AsnString(AsnIdentifier id, ReadOnlyMemory<byte> buffer, string value)
            : base(id, buffer)
        {
            Value = value.Quote(((value.IndexOf(",") != -1) ? "\"" : string.Empty));
        }
    }
}
