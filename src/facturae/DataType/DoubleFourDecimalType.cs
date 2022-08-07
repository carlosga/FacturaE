// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType;

public struct DoubleFourDecimalType
    : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IXmlSerializable
{
    public static readonly DoubleFourDecimalType MaxValue = new(double.MaxValue);
    public static readonly DoubleFourDecimalType MinValue = new(double.MinValue);

    public static bool GreaterThan(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        return left > right;
    }

    public static bool GreaterThanOrEqual(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        return left >= right;
    }

    public static bool LessThan(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        return left < right;
    }

    public static bool LessThanOrEqual(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        return left <= right;
    }

    public static bool NotEquals(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        return left != right;
    }

    public static DoubleFourDecimalType Parse(string value)
    {
        return new DoubleFourDecimalType(double.Parse(value));
    }

    public static bool operator ==(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        bool equals = false;

        if (left.Value == right.Value)
        {
            equals = true;
        }

        return equals;
    }

    public static bool operator !=(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        bool notequals = false;

        if (left.Value != right.Value)
        {
            notequals = true;
        }

        return notequals;
    }

    public static bool operator >(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        bool greater = false;

        if (left.Value > right.Value)
        {
            greater = true;
        }

        return greater;
    }

    public static bool operator >=(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        bool greater = false;

        if (left.Value >= right.Value)
        {
            greater = true;
        }

        return greater;
    }

    public static bool operator <(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        bool less = false;

        if (left.Value < right.Value)
        {
            less = true;
        }

        return less;
    }

    public static bool operator <=(DoubleFourDecimalType left, DoubleFourDecimalType right)
    {
        bool less = false;

        if (left.Value <= right.Value)
        {
            less = true;
        }

        return less;
    }

    public static implicit operator double(DoubleFourDecimalType x)
    {
        return x.Value;
    }

    public static implicit operator DoubleFourDecimalType(double x)
    {
        return new DoubleFourDecimalType(x);
    }

    [XmlIgnore]
    public double Value { get; private set; }

    public DoubleFourDecimalType(double value)
    {
        Value = value;
    }

    public DoubleFourDecimalType(decimal value)
    {
        Value = (double)value;
    }

    public override int GetHashCode()
    {
        return 207501131 ^ Value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is not null and DoubleFourDecimalType value && value == this;
    }

    public override string ToString()
    {
        return Value.ToString("F4", System.Globalization.CultureInfo.InvariantCulture);
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
        return Value.ToString("F4", formatProvider);
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
        return ToString();
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
