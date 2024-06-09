﻿// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType;

public struct DoubleTwoDecimalType
    : IComparable, IFormattable, IConvertible, IComparable<decimal>, IEquatable<decimal>, IXmlSerializable
{
    public static readonly DoubleTwoDecimalType MaxValue = new(decimal.MaxValue);
    public static readonly DoubleTwoDecimalType	MinValue = new(decimal.MinValue);

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
        return new DoubleTwoDecimalType(decimal.Parse(value));
    }

    public static bool operator ==(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left.Value == right.Value;
    }

    public static bool operator !=(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left.Value != right.Value;
    }

    public static bool operator >(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left.Value > right.Value;
    }

    public static bool operator >=(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left.Value >= right.Value;
    }

    public static bool operator <(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left.Value < right.Value;
    }

    public static bool operator <=(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
    {
        return left.Value <= right.Value;
    }

    public static implicit operator decimal(DoubleTwoDecimalType x)
    {
        return x.Value;
    }

    public static implicit operator DoubleTwoDecimalType(decimal x)
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
    public decimal Value { get; private set; }

    public DoubleTwoDecimalType(decimal value)
    {
        Value = value;
    }

    public override readonly int GetHashCode()
    {
        return 207501130 ^ Value.GetHashCode();
    }

    public override readonly bool Equals(object obj)
    {
        return obj is not null && obj is DoubleTwoDecimalType value && value == this;
    }

    public override readonly string ToString()
    {
        return Value.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
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
        return Value.ToString("F2", formatProvider);
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
        return Value;
    }

    readonly double IConvertible.ToDouble(IFormatProvider provider)
    {
        return Convert.ToDouble(Value);
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

    readonly int IComparable<decimal>.CompareTo(decimal other)
    {
        return CompareTo(other);
    }

    public readonly bool Equals(decimal other)
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
            Value = reader.ReadElementContentAsDecimal();
        }
    }

    public readonly void WriteXml(XmlWriter writer)
    {
        writer?.WriteString(ToString());
    }
}
