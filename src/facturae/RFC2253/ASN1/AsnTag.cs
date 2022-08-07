// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

/// <summary>
/// http://en.wikipedia.org/wiki/X.690
/// </summary>
public enum AsnTag
{
    EOC = 0x00,
    Boolean = 0x01,
    Integer = 0x02,
    BitString = 0x03,
    OctetString = 0x04,
    Null = 0x05,
    ObjectIdentifier = 0x06,
    ObjectDescriptor = 0x07,
    External = 0x08,
    Real = 0x09,
    Enumerated = 0x0A,
    EmbeddedPdv = 0x0B,
    UTF8String = 0x0C,
    RelativeObjectIdentifier = 0x0D,
    SequenceAndSequenceOf = 0x10,
    SetAndSetOf = 0x11,
    NumericString = 0x12,
    PrintableString = 0x13,
    T61String = 0x14,
    VideotexString = 0x15,
    IA5String = 0x16,
    UTCTime = 0x17,
    GeneralizedTime = 0x18,
    GraphicString = 0x19,
    VisibleString = 0x1A,
    GeneralString = 0x1B,
    UniversalString = 0x1C,
    CharacterString = 0x1D,
    BmpString = 0x1E,
    LongForm = 0x1F,
}
