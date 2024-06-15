// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace ASN1;

/// <summary>
/// https://github.com/notpeter/openjdk/blob/1917a46616cf12591ee26d38a4adbc5f105f38cb/jdk/test/javax/security/auth/x500/X500Principal/NameFormat.java
/// </summary>
internal sealed class AsnPrintableString : AsnString
{
    public AsnPrintableString(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
        : base(id, buffer, Encoding.ASCII.GetString(buffer.Span))
    {
    }
}
