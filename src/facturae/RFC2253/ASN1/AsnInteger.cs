// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1
{
    public sealed class AsnInteger
        : AsnValueObject<int>
    {
        public AsnInteger(AsnIdentifier id, byte[] buffer)
            : base(id, buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                Value = (Value << 8) | (buffer[i] & 0xFF);
            }
        }
    }
}
