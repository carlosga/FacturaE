// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

public abstract class AsnObject
{
    public AsnIdentifier Id
    {
        get;
        private set;
    }

    public ReadOnlyMemory<byte> RawData
    {
        get;
        protected set;
    }

    protected AsnObject(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
    {
        Id      = id;
        RawData = buffer;
    }
}
