// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;

namespace ASN1;

public sealed class AsnSet : AsnObject, IEnumerable<AsnObject>
{
    private readonly List<AsnObject> _objects = [];

    public AsnSet(AsnIdentifier id, ReadOnlyMemory<byte> buffer) : base(id, buffer)
    {
    }

    internal void Add(AsnObject asnObject) => _objects.Add(asnObject);

    public IEnumerator<AsnObject> GetEnumerator() => _objects.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _objects.GetEnumerator();
}