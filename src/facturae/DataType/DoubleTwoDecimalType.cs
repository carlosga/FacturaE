// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType;

public struct DoubleTwoDecimalType
    : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IXmlSerializable
{
    public static readonly DoubleTwoDecimalType MaxValue = new(double.MaxValue);
    public static readonly DoubleTwoDecimalType	MinValue = new(double.MinValue);

    public static bool GreaterThan(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left > right;
    }

    public static bool GreaterThanOrEqual(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left >= right;
    }

    public static bool LessThan(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left < right;
    }

    public static bool LessThanOrEqual(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left <= right;
    }

    public static bool NotEquals(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left != right;
    }

    public static DoubleTwoDecimalType Parse(string value)
    {
        return new DoubleTwoDecimalType(double.Parse(value));
    }

    public static bool operator ==(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        bool equals = false;

        if (left.Value == right.Value)
        {
            equals = true;
        }

        return equals;
    }

    public static bool operator !=(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        bool notequals = false;

        if (left.Value != right.Value)
        {
            notequals = true;
        }

        return notequals;
    }

    public static bool operator >(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        bool greater = false;

        if (left.Value > right.Value)
        {
            greater = true;
        }

        return greater;
    }

    public static bool operator >=(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        bool greater = false;

        if (left.Value >= right.Value)
        {
            greater = true;
        }

        return greater;
    }

    public static bool operator <(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        bool less = false;

        if (left.Value < right.Value)
        {
            less = true;
        }

        return less;
    }

    public static bool operator <=(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        bool less = false;

        if (left.Value <= right.Value)
        {
            less = true;
        }

        return less;
    }

    public static implicit operator double(DoubleTwoDecimalType x)
    {
        return x.Value;
    }

    public static implicit operator DoubleTwoDecimalType(double x)
    {
        return new DoubleTwoDecimalType(x);
    }

    public static implicit operator DoubleTwoDecimalType(DoubleSixDecimalType x)
    {
        return new DoubleTwoDecimalType(x.Value);
    }

    public static implicit operator DoubleTwoDecimalType(DoubleUpToEightDecimalType x)
    {
        return new DoubleTwoDecimalType(x.Value);
    }

    [XmlIgnore]
    public double Value { get; private set; }

    public DoubleTwoDecimalType(double value)
    {
        Value = value;
    }

    public DoubleTwoDecimalType(decimal value)
    {
        Value = (double)value;
    }

    public override int GetHashCode()
    {
        return 207501130 ^ Value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is not null && obj is DoubleTwoDecimalType value && value == this;
    }

    public override string ToString()
    {
        return Value.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
    }

    public string ToString(string format)
    {
        return Value.ToString(format);
    }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(obj);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return Value.ToString("F2", formatProvider);
    }

    TypeCode IConvertible.GetTypeCode()
    {
        return TypeCode.Object;
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
        return Convert.ToBoolean(Value);
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
        return Convert.ToByte(Value);
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
        return Convert.ToChar(Value);
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
        return Convert.ToDateTime(Value);
    }

    decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
        return Convert.ToDecimal(Value);
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
        return Value;
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
        return Convert.ToInt16(Value);
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
        return Convert.ToInt32(Value);
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
        return Convert.ToInt64(Value);
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
        return Convert.ToSByte(Value);
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
        return Convert.ToSingle(Value);
    }

    string IConvertible.ToString(IFormatProvider provider)
    {
        return this.ToString();
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
        return Convert.ChangeType(Value, conversionType);
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
        return Convert.ToUInt16(Value);
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
        return Convert.ToUInt32(Value);
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
        return Convert.ToUInt64(Value);
    }

    int IComparable<double>.CompareTo(double other)
    {
        return CompareTo(other);
    }

    public bool Equals(double other)
    {
        return Equals(other);
    }

    public XmlSchema GetSchema()
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

    public void WriteXml(XmlWriter writer)
    {
        writer?.WriteString(ToString());
    }
}
