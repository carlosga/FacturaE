// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Text;

namespace ASN1
{
    public sealed class AsnObjectIdentifier
        : AsnValueObject<string>
    {
        public AsnObjectIdentifier(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
            : base(id, buffer)
        {
            // http://msdn.microsoft.com/en-us/library/windows/desktop/bb540809(v=vs.85).aspx
            // The first two nodes of the OID are encoded onto a single byte.
            // The first node is multiplied by the decimal 40 and the result is added to the value of the second node.
            // Node values less than or equal to 127 are encoded on one byte.
            // Node values greater than or equal to 128 are encoded on multiple bytes.
            // Bit 7 of the leftmost byte is set to one. Bits 0 through 6 of each byte contains the encoded value.

            var value = new StringBuilder(buffer.Length);
            var node  = buffer.Span[0];

            value.Append(((int)(node / 40)).ToString(CultureInfo.InvariantCulture));
            value.Append(".");
            value.Append(((int)(node % 40)).ToString(CultureInfo.InvariantCulture));

            for (int i = 1; i < buffer.Length;)
            {
                int  sid    = 0;
                int  octect = buffer.Span[i];
                bool vlq    = ((octect & 0x80) == 0x80);

                if (vlq)
                {
                    int weight = 0;
                    int j      = i;
                    do
                    {
                        weight++;
                    } while (((buffer.Span[++j] & 0x80) == 0x80));
                    do
                    {
                        sid |= (buffer.Span[i++] & 0x7F) << (7 * weight);
                    } while (--weight >= 0);
                }
                else
                {
                    i++;
                    sid = octect;
                }

                value.Append(".");
                value.Append(sid.ToString(CultureInfo.InvariantCulture));
                
                Value = value.ToString();
            }
        }
    }
}
