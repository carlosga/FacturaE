// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace ASN1
{
    /// <summary>
    /// ASN1 References:
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb648643%28v=vs.85%29.aspx
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb540809(v=vs.85).aspx
    /// </summary>
    public sealed class AsnDecoder
    {
        private readonly ReadOnlyMemory<byte> _memory;
        private int                           _position;

        public bool EOF => (_position >= _memory.Length);

        public AsnDecoder(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            _memory = new ReadOnlyMemory<byte>(buffer);
        }

        public AsnObject MoveNext() => ReadObject();

        private AsnObject ReadObject()
        {
            var id     = ReadAsnIdentifier();
            var length = ReadLength();

            // TODO: Handle custom constructed types ( for example constructed bit strings )
            return this.ReadPrimitiveType(id, length);
        }

        private AsnObject ReadPrimitiveType(AsnIdentifier id, int length)
        {
            switch (id.Tag)
            {
                case AsnTag.Null:
                case AsnTag.EOC:
                    return null;

                case AsnTag.ObjectIdentifier:
                    return ReadAsnObject<AsnObjectIdentifier>(id, length);

                case AsnTag.SequenceAndSequenceOf:
                    return ReadSequence(id, length);

                case AsnTag.SetAndSetOf:
                    return ReadSet(id, length);

                case AsnTag.Integer:
                    return ReadAsnObject<AsnInteger>(id, length);

                case AsnTag.UTCTime:
                    return ReadAsnObject<AsnUTCTime>(id, length);

                case AsnTag.BitString:
                    return ReadAsnObject<AsnBitString>(id, length);

                case AsnTag.BmpString:
                    return ReadAsnObject<AsnBmpString>(id, length);

                case AsnTag.IA5String:
                    return ReadAsnObject<AsnIA5String>(id, length);

                case AsnTag.OctetString:
                    return ReadAsnObject<AsnOctetString>(id, length);

                case AsnTag.PrintableString:
                    return ReadAsnObject<AsnPrintableString>(id, length);

                case AsnTag.T61String:
                    return ReadAsnObject<AsnT61String>(id, length);

                case AsnTag.UTF8String:
                    return ReadAsnObject<AsnUTF8String>(id, length);
            }

            throw new InvalidOperationException();
        }

        private AsnSequence ReadSequence(AsnIdentifier id, int length)
        {
            var sequence = new AsnSequence(id, _memory.Slice(_position + length));
            var ends     = _position + length;

            while (!EOF && _position != ends)
            {
                sequence.Add(ReadObject());
            }

            return sequence;
        }

        private AsnSet ReadSet(AsnIdentifier id, int length)
        {
            var set  = new AsnSet(id, PeekBytes(length));
            var ends = _position + length;

            while (!EOF && _position != ends)
            {
                set.Add(ReadObject());
            }

            return set;
        }

        private T ReadAsnObject<T>(AsnIdentifier id, int length)
            where T : AsnObject
        {
            var rawData = ReadBytes(length);

            return (T)Activator.CreateInstance(typeof(T), new object[] { id, rawData });
        }

        private AsnIdentifier ReadAsnIdentifier() => new AsnIdentifier(ReadByte());

        // Lenght. Two forms:
        // 
        // Short form. One octet. Bit 8 has value "0" and bits 7-1 give the length.
        // Long form. Two to 127 octets. Bit 8 of first octet has value "1" and bits 7-1 give the number of additional length octets. 
        //            Second and following octets give the length, base 256, most significant digit first.

        private int ReadLength()
        {
            int length = ReadByte();

            if ((length & 0x80) == 0x80)
            {
                int count = length & 0x7F;
                length = 0;
                for (int i = 0; i < count; i++)
                {
                    length = length * 256 + ReadByte();
                }
            }

            return length;
        }

        private byte ReadByte()
        {
            return _memory.Slice(_position++, 1).Span[0];
        }

        private ReadOnlyMemory<byte> ReadBytes(int count)
        {
            var slice = PeekBytes(count);

            _position += count;

            return slice;
        }

        private ReadOnlyMemory<byte> PeekBytes(int count)
        {
            return _memory.Slice(_position, count);
        }
    }
}
