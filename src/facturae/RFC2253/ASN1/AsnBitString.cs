// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;

namespace ASN1
{
    public sealed class AsnBitString
        : AsnString
    {
        private static string Encode(ReadOnlyMemory<byte> buffer)
        {
            int bitCount = ((buffer.Length - 1) * 8) - buffer.Span[0];
            var result   = new StringBuilder(buffer.Length); 

            if (bitCount > 0)
            {
                for (int i = 1; i < buffer.Length; i++)
                {
                    int current = buffer.Span[i];

                    for (byte j = 8; j > 0; j--)
                    {
                        if (bitCount > 0)
                        {
                            result.Append(((current.IsBitSet(j)) ? "1" : "0"));
                        }

                        bitCount--;
                    }

                    if (bitCount == 0)
                    {
                        break;
                    }
                }
            }
            
            return result.ToString();
        }

        public AsnBitString(AsnIdentifier id, ReadOnlyMemory<byte> buffer)
            : base(id, buffer, Encode(buffer))
        {
        }
    }
}
