// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType
{
    public struct DoubleTwoDecimalType 
        : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IXmlSerializable
    {
        public static readonly DoubleTwoDecimalType MaxValue = new DoubleTwoDecimalType(Double.MaxValue);
        public static readonly DoubleTwoDecimalType	MinValue = new DoubleTwoDecimalType(Double.MinValue);

        public static bool GreaterThan(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
        {
            return (left > right);
        }

        public static bool GreaterThanOrEqual(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
        {
            return (left >= right);
        }

        public static bool LessThan(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
        {
            return (left < right);
        }

        public static bool LessThanOrEqual(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
        {
            return (left <= right);
        }

        public static bool NotEquals(DoubleTwoDecimalType left, DoubleTwoDecimalType right)
        {
            return (left != right);
        }

        public static DoubleTwoDecimalType Parse(string value)
        {
            return new DoubleTwoDecimalType(Double.Parse(value));
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

        private double _value;

        [XmlIgnore]
        public double Value
        {
            get { return _value; }
        }

        public DoubleTwoDecimalType(double value)
        {
            _value = value;
        }

        public DoubleTwoDecimalType(decimal value)
        {
            _value = (double)value;
        }

        public override int GetHashCode()
        {
            return 207501130 ^ _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is DoubleTwoDecimalType)
            {
                return ((DoubleTwoDecimalType)obj) == this;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return _value.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        public String ToString(String format)
        {
            return _value.ToString(format);
        }

        public int CompareTo(object obj)
        {
            return _value.CompareTo(obj);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _value.ToString("F2", formatProvider);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return _value;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_value);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_value);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(_value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_value);
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
            if (reader != null)
            {
                _value = reader.ReadElementContentAsDouble();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer?.WriteString(ToString());
        }
    }
}
