/* FacturaE - The MIT License (MIT)
 * 
 * Copyright (c) 2012-2014 Carlos Guzmán Álvarez
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FacturaE.DataType
{
    public struct DoubleTwoDecimalType 
        : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IXmlSerializable
    {
        #region · Static Fields ·

        public static readonly DoubleTwoDecimalType MaxValue = new DoubleTwoDecimalType(Double.MaxValue);
        public static readonly DoubleTwoDecimalType	MinValue = new DoubleTwoDecimalType(Double.MinValue);

        #endregion

        #region · Static Methods ·

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

        #endregion

        #region · Operators ·

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

        #endregion

        #region · Fields ·

        private double value;

        #endregion

        #region · Properties ·

        [XmlIgnore]
        public double Value
        {
            get { return this.value; }
        }

        #endregion

        #region · Constructors ·

        public DoubleTwoDecimalType(double value)
        {
            this.value = value;
        }

        public DoubleTwoDecimalType(decimal value)
        {
            this.value = (double)value;
        }

        #endregion

        #region · Overriden Methods ·

        public override int GetHashCode()
        {
            return 207501130 ^ this.value.GetHashCode();
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
            return this.value.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        public String ToString(String format)
        {
            return this.Value.ToString(format);
        }

        #endregion

        #region · IComparable methods ·

        public int CompareTo(object obj)
        {
            return this.value.CompareTo(obj);
        }

        #endregion

        #region · IFormattable Members ·

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.Value.ToString("F2", formatProvider);
        }

        #endregion

        #region · IConvertible Members ·

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(this.Value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this.Value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(this.Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(this.Value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(this.Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return this.Value;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this.Value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this.Value);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(this.Value);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this.Value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.Value);
        }

        #endregion

        #region · IComparable<double> Members ·

        int IComparable<double>.CompareTo(double other)
        {
            return this.CompareTo(other);
        }

        #endregion

        #region · IEquatable<double> Members ·

        public bool Equals(double other)
        {
            return this.Equals(other);
        }

        #endregion

        #region · IXmlSerializable Members ·

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader != null)
            {
                this.value = XmlConvert.ToDouble(reader.ReadString());
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            if (writer != null)
            {
                writer.WriteString(this.ToString());
            }
        }

        #endregion
    }
}
