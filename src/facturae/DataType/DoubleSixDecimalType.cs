// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType;

public struct DoubleSixDecimalType
    : IComparable, IFormattable, IConvertible, IComparable<decimal>, IEquatable<decimal>, IXmlSerializable
{
    public static readonly DoubleSixDecimalType MaxValue = new(decimal.MaxValue);
    public static readonly DoubleSixDecimalType	MinValue = new(decimal.MinValue);

    public static bool GreaterThan(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left > right;
    }

    public static bool GreaterThanOrEqual(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left >= right;
    }

    public static bool LessThan(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left < right;
    }

    public static bool LessThanOrEqual(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left <= right;
    }

    public static bool NotEquals(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left != right;
    }

    public static DoubleSixDecimalType Parse(string value)
    {
        return new DoubleSixDecimalType(decimal.Parse(value));
    }

    public static bool operator ==(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left._value == right._value;
    }

    public static bool operator !=(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left._value != right._value;
    }

    public static bool operator >(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left._value > right._value;
    }

    public static bool operator >=(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left._value >= right._value;
    }

    public static bool operator <(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left._value < right._value;
    }

    public static bool operator <=(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        return left._value <= right._value;
    }

    public static implicit operator decimal(DoubleSixDecimalType x)
    {
        return x._value;
    }

    public static implicit operator DoubleSixDecimalType(decimal x)
    {
        return new DoubleSixDecimalType(x);
    }

    public static implicit operator DoubleSixDecimalType(DoubleTwoDecimalType x)
    {
        return new DoubleFourDecimalType((decimal)x);
    }

    public static implicit operator DoubleSixDecimalType(DoubleFourDecimalType x)
    {
        return new DoubleFourDecimalType((decimal)x);
    }

    public static implicit operator DoubleSixDecimalType(DoubleUpToEightDecimalType x)
    {
        return new DoubleFourDecimalType((decimal)x);
    }

    private decimal _value;

    public DoubleSixDecimalType(decimal value)
    {
        _value = value;
    }

    public override readonly int GetHashCode()
    {
        return 207501132 ^ _value.GetHashCode();
    }

    public override readonly bool Equals(object obj)
    {
        return obj is not null and DoubleSixDecimalType dvalue && dvalue == this;
    }

    public override readonly string ToString()
    {
        return _value.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
    }

    public readonly string ToString(string format)
    {
        return _value.ToString(format);
    }

    public readonly int CompareTo(object obj)
    {
        return _value.CompareTo(obj);
    }

    public readonly string ToString(string format, IFormatProvider formatProvider)
    {
        return _value.ToString("F6", formatProvider);
    }

    readonly TypeCode IConvertible.GetTypeCode()
    {
        return TypeCode.Object;
    }

    readonly bool IConvertible.ToBoolean(IFormatProvider provider)
    {
        return Convert.ToBoolean(_value);
    }

    readonly byte IConvertible.ToByte(IFormatProvider provider)
    {
        return Convert.ToByte(_value);
    }

    readonly char IConvertible.ToChar(IFormatProvider provider)
    {
        return Convert.ToChar(_value);
    }

    readonly DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
        return Convert.ToDateTime(_value);
    }

    readonly decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
        return _value;
    }

    readonly double IConvertible.ToDouble(IFormatProvider provider)
    {
        return Convert.ToDouble(_value);
    }

    readonly short IConvertible.ToInt16(IFormatProvider provider)
    {
        return Convert.ToInt16(_value);
    }

    readonly int IConvertible.ToInt32(IFormatProvider provider)
    {
        return Convert.ToInt32(_value);
    }

    readonly long IConvertible.ToInt64(IFormatProvider provider)
    {
        return Convert.ToInt64(_value);
    }

    readonly sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
        return Convert.ToSByte(_value);
    }

    readonly float IConvertible.ToSingle(IFormatProvider provider)
    {
        return Convert.ToSingle(_value);
    }

    readonly string IConvertible.ToString(IFormatProvider provider)
    {
        return this.ToString();
    }

    readonly object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
        return Convert.ChangeType(_value, conversionType);
    }

    readonly ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
        return Convert.ToUInt16(_value);
    }

    readonly uint IConvertible.ToUInt32(IFormatProvider provider)
    {
        return Convert.ToUInt32(_value);
    }

    readonly ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
        return Convert.ToUInt64(_value);
    }

    readonly int IComparable<decimal>.CompareTo(decimal other)
    {
        return CompareTo(other);
    }

    public readonly bool Equals(decimal other)
    {
        return Equals(other);
    }

    readonly XmlSchema IXmlSerializable.GetSchema()
    {
        return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        if (reader is not null)
        {
            _value = reader.ReadElementContentAsDecimal();
        }
    }

    readonly void IXmlSerializable.WriteXml(XmlWriter writer)
    {
        writer?.WriteString(ToString());
    }
}
