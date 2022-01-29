using System;
using System.Collections.Generic;

namespace HexMask
{
    public struct HexString : IEquatable<HexString>
    {
        internal readonly byte[] bytes;

        public HexString(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public HexString(string hexString)
        {
            bytes = HexStringParser.Parse(hexString);
        }

        public int ByteCount => bytes?.Length ?? 0;

        public int Length => (bytes?.Length ?? 0) * 2;

        public byte this[int index] => bytes[index];

        public override string ToString()
        {
            return BitConverter.ToString(bytes, 0, bytes.Length);
        }

        public byte[] ToArray()
        {
            var result = new byte[this.bytes.Length];
            Array.Copy(this.bytes, result, result.Length);
            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is HexString @string && Equals(@string);
        }

        public bool Equals(HexString other)
        {
            return EqualityComparer<byte[]>.Default.Equals(bytes, other.bytes);
        }

        public override int GetHashCode()
        {
            return -119561052 + EqualityComparer<byte[]>.Default.GetHashCode(bytes);
        }

        public static bool operator ==(HexString left, HexString right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexString left, HexString right)
        {
            return !(left == right);
        }
    }
}
