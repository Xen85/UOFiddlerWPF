using System;

namespace OpenUO.MapMaker.Elements.BaseTypes.Base
{
    [Serializable]
    public class Id : IEquatable<Id>, IEquatable<int>, IConvertible
    {
        public Id()
        {
            Value = 0;
        }

        public Int32 Value { get; set; }

        #region Implementation of IComparable

       
        #endregion
        
        public bool Equals(Id other)
        {
            return (other.Value == Value);
        }

        public bool Equals(int other)
        {
            return other == Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #region Implementation of IConvertible

        public TypeCode GetTypeCode()
        {
            return Value.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            if (Value > 0)
                return true;
            return false;
        }

        public char ToChar(IFormatProvider provider)
        {
            return (char) Value;
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte) Value;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (Byte) Value;
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (short) Value;
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort) Value;
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Value;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return (UInt32) Value;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Value;
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return (UInt64) Value;
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Value;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Value;
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Value;
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return new DateTime(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return Value.ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}