// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

/// <summary>
/// ASN1 References:
/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb648643%28v=vs.85%29.aspx
/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb540809(v=vs.85).aspx
/// </summary>
public sealed class AsnDecoder
{
    private readonly ReadOnlyMemory<byte> _memory;
    private int                           _position;

    public bool EOF => _position >= _memory.Length;

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
        return ReadPrimitiveType(id, length);
    }

    private AsnObject ReadPrimitiveType(AsnIdentifier id, int length)
    {
        return id.Tag switch
        {
            AsnTag.Null or AsnTag.EOC    => null,
            AsnTag.ObjectIdentifier      => ReadAsnObject<AsnObjectIdentifier>(id, length),
            AsnTag.SequenceAndSequenceOf => ReadSequence(id, length),
            AsnTag.SetAndSetOf           => ReadSet(id, length),
            AsnTag.Integer               => ReadAsnObject<AsnInteger>(id, length),
            AsnTag.UTCTime               => ReadAsnObject<AsnUTCTime>(id, length),
            AsnTag.BitString             => ReadAsnObject<AsnBitString>(id, length),
            AsnTag.BmpString             => ReadAsnObject<AsnBmpString>(id, length),
            AsnTag.IA5String             => ReadAsnObject<AsnIA5String>(id, length),
            AsnTag.OctetString           => ReadAsnObject<AsnOctetString>(id, length),
            AsnTag.PrintableString       => ReadAsnObject<AsnPrintableString>(id, length),
            AsnTag.T61String             => ReadAsnObject<AsnT61String>(id, length),
            AsnTag.UTF8String            => ReadAsnObject<AsnUTF8String>(id, length),
            _                            => throw new InvalidOperationException(),
        };
    }

    private AsnSequence ReadSequence(AsnIdentifier id, int length)
    {
        var sequence = new AsnSequence(id, _memory[(_position + length)..]);
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

    private T ReadAsnObject<T>(AsnIdentifier id, int length) where T : AsnObject
    {
        var rawData    = ReadBytes(length);
        var targetType = typeof(T);

        if (targetType == typeof(AsnObjectIdentifier))
        {
            return new AsnObjectIdentifier(id, rawData) as T;
        }
        else if (targetType == typeof(AsnInteger))
        {
            return new AsnInteger(id, rawData) as T;
        }
        else if (targetType == typeof(AsnUTCTime))
        {
            return new AsnUTCTime(id, rawData) as T;
        }
        else if (targetType == typeof(AsnBitString))
        {
            return new AsnBitString(id, rawData) as T;
        }
        else if (targetType == typeof(AsnBmpString))
        {
            return new AsnBmpString(id, rawData) as T;
        }
        else if (targetType == typeof(AsnIA5String))
        {
            return new AsnIA5String(id, rawData) as T;
        }
        else if (targetType == typeof(AsnOctetString))
        {
            return new AsnOctetString(id, rawData) as T;
        }
        else if (targetType == typeof(AsnPrintableString))
        {
            return new AsnPrintableString(id, rawData) as T;
        }
        else if (targetType == typeof(AsnT61String))
        {
            return new AsnT61String(id, rawData) as T;
        }
        else if (targetType == typeof(AsnUTF8String))
        {
            return new AsnUTF8String(id, rawData) as T;
        }
        else
        {
            throw new ArgumentException($"Uknow type {targetType.FullName}");
        }
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
