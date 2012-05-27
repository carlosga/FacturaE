using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ElectronicInvoice.DataType
{
    public struct DoubleSixDecimalType 
        : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IXmlSerializable
    {
        #region · Static Fields ·

        public static readonly DoubleSixDecimalType MaxValue = new DoubleSixDecimalType(Double.MaxValue);
        public static readonly DoubleSixDecimalType	MinValue = new DoubleSixDecimalType(Double.MinValue);

        #endregion

        #region · Static Methods ·

        public static bool GreatherThan(DoubleSixDecimalType x, DoubleSixDecimalType y)
        {
            return (x > y);
        }

        public static bool GreatherThanOrEqual(DoubleSixDecimalType x, DoubleSixDecimalType y)
        {
            return (x >= y);
        }

        public static bool LessThan(DoubleSixDecimalType x, DoubleSixDecimalType y)
        {
            return (x < y);
        }

        public static bool LessThanOrEqual(DoubleSixDecimalType x, DoubleSixDecimalType y)
        {
            return (x <= y);
        }

        public static bool NotEquals(DoubleSixDecimalType x, DoubleSixDecimalType y)
        {
            return (x != y);
        }

        public static DoubleSixDecimalType Parse(string s)
        {
            return new DoubleSixDecimalType(Double.Parse(s));
        }

        #endregion

        #region · Operators ·

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

        public DoubleSixDecimalType(double value)
        {
            this.value = value;
        }

        public DoubleSixDecimalType(decimal value)
        {
            this.value = (double)value;
        }

        #endregion

        #region · Overriden Methods ·

        public override int GetHashCode()
        {
            return 207501132 ^ this.value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is DoubleSixDecimalType)
            {
                return ((DoubleSixDecimalType)obj) == this;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.value.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
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
            return this.Value.ToString("F6", formatProvider);
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
            this.value = XmlConvert.ToDouble(reader.ReadString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }

        #endregion
    }
}
