// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace ASN1
{
    public sealed class AsnUTF8String
        : AsnString
    {
        public AsnUTF8String(AsnIdentifier id, byte[] buffer)
            : base(id, buffer, Encoding.UTF8.GetString(buffer, 0, buffer.Length))
        {
        }
    }
}
