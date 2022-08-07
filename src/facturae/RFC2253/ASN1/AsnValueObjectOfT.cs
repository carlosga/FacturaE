// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

public abstract class AsnValueObject<T> : AsnObject, IAsnValueObject<T>
{
    public T Value
    {
        get;
        protected set;
    }

    object IAsnValueObject.Value
    {
        get { return Value; }
    }

    protected AsnValueObject(AsnIdentifier id, ReadOnlyMemory<byte> buffer) : base(id, buffer)
    {
    }

    public override string ToString() => Value.ToString();
}
