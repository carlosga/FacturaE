// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType;

public struct DoubleSixDecimalType
    : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IXmlSerializable
{
    public static readonly DoubleSixDecimalType MaxValue = new(double.MaxValue);
    public static readonly DoubleSixDecimalType	MinValue = new(double.MinValue);

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
        return new DoubleSixDecimalType(double.Parse(value));
    }

    public static bool operator ==(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        bool equals = false;

        if (left.Value == right.Value)
        {
            equals = true;
        }

        return equals;
    }

    public static bool operator !=(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        bool notequals = false;

        if (left.Value != right.Value)
        {
            notequals = true;
        }

        return notequals;
    }

    public static bool operator >(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        bool greater = false;

        if (left.Value > right.Value)
        {
            greater = true;
        }

        return greater;
    }

    public static bool operator >=(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        bool greater = false;

        if (left.Value >= right.Value)
        {
            greater = true;
        }

        return greater;
    }

    public static bool operator <(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        bool less = false;

        if (left.Value < right.Value)
        {
            less = true;
        }

        return less;
    }

    public static bool operator <=(DoubleSixDecimalType left, DoubleSixDecimalType right)
    {
        bool less = false;

        if (left.Value <= right.Value)
        {
            less = true;
        }

        return less;
    }

    public static implicit operator double(DoubleSixDecimalType x)
    {
        return x.Value;
    }

    public static implicit operator DoubleSixDecimalType(double x)
    {
        return new DoubleSixDecimalType(x);
    }

    public static implicit operator DoubleSixDecimalType(DoubleTwoDecimalType x)
    {
        return new DoubleSixDecimalType(x.Value);
    }

    [XmlIgnore]
    public double Value { get; private set; }

    public DoubleSixDecimalType(double value)
    {
        Value = value;
    }

    public DoubleSixDecimalType(decimal value)
    {
        Value = (double)value;
    }

    public override readonly int GetHashCode()
    {
        return 207501132 ^ Value.GetHashCode();
    }

    public override readonly bool Equals(object obj)
    {
        return obj is not null and DoubleSixDecimalType dvalue && dvalue == this;
    }

    public override readonly string ToString()
    {
        return Value.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
    }

    public readonly string ToString(string format)
    {
        return Value.ToString(format);
    }

    public readonly int CompareTo(object obj)
    {
        return Value.CompareTo(obj);
    }

    public readonly string ToString(string format, IFormatProvider formatProvider)
    {
        return Value.ToString("F6", formatProvider);
    }

    readonly TypeCode IConvertible.GetTypeCode()
    {
        return TypeCode.Object;
    }

    readonly bool IConvertible.ToBoolean(IFormatProvider provider)
    {
        return Convert.ToBoolean(Value);
    }

    readonly byte IConvertible.ToByte(IFormatProvider provider)
    {
        return Convert.ToByte(Value);
    }

    readonly char IConvertible.ToChar(IFormatProvider provider)
    {
        return Convert.ToChar(Value);
    }

    readonly DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
        return Convert.ToDateTime(Value);
    }

    readonly decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
        return Convert.ToDecimal(Value);
    }

    readonly double IConvertible.ToDouble(IFormatProvider provider)
    {
        return Value;
    }

    readonly short IConvertible.ToInt16(IFormatProvider provider)
    {
        return Convert.ToInt16(Value);
    }

    readonly int IConvertible.ToInt32(IFormatProvider provider)
    {
        return Convert.ToInt32(Value);
    }

    readonly long IConvertible.ToInt64(IFormatProvider provider)
    {
        return Convert.ToInt64(Value);
    }

    readonly sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
        return Convert.ToSByte(Value);
    }

    readonly float IConvertible.ToSingle(IFormatProvider provider)
    {
        return Convert.ToSingle(Value);
    }

    readonly string IConvertible.ToString(IFormatProvider provider)
    {
        return this.ToString();
    }

    readonly object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
        return Convert.ChangeType(Value, conversionType);
    }

    readonly ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
        return Convert.ToUInt16(Value);
    }

    readonly uint IConvertible.ToUInt32(IFormatProvider provider)
    {
        return Convert.ToUInt32(Value);
    }

    readonly ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
        return Convert.ToUInt64(Value);
    }

    readonly int IComparable<double>.CompareTo(double other)
    {
        return CompareTo(other);
    }

    public readonly bool Equals(double other)
    {
        return Equals(other);
    }

    public readonly XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        if (reader is not null)
        {
            Value = reader.ReadElementContentAsDouble();
        }
    }

    public readonly void WriteXml(XmlWriter writer)
    {
        writer?.WriteString(ToString());
    }
}
