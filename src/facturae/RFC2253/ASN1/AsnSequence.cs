﻿// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;

namespace ASN1
{
    public sealed class AsnSequence
        : AsnObject, IEnumerable<AsnObject>
    {
        private readonly List<AsnObject> _objects;

        public AsnSequence(AsnIdentifier id, byte[] buffer)
            : base(id, buffer)
        {
            _objects = new List<AsnObject>();
        }

        internal void Add(AsnObject asnObject) => _objects.Add(asnObject);

        public IEnumerator<AsnObject> GetEnumerator() => _objects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _objects.GetEnumerator();
    }
}
