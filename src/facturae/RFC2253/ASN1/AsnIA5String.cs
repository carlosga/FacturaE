// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace ASN1
{
    public sealed class AsnIA5String
        : AsnString
    {
        public AsnIA5String(AsnIdentifier id, byte[] buffer)
            : base(id, buffer, Encoding.ASCII.GetString(buffer, 0, buffer.Length))
        {
        }
    }
}
