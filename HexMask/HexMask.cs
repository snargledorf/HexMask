using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace HexMask
{
    public struct HexMask : IEquatable<HexMask>
    {
        public HexMask(byte[] bytes) : this(new HexString(bytes))
        {
        }

        public HexMask(string hexMask) : this(new HexString(hexMask))
        {
        }

        public HexMask(HexString hexString)
        {
            HexString = hexString;

            BitArray hexBits = new BitArray(hexString.bytes);
            
            BitCount = GetBitCount(hexBits);
            var bits = new bool[BitCount];

            for (int i = 0; i < BitCount; i++)
                bits[i] = hexBits[i];

            Bits = Array.AsReadOnly(bits);
        }

        public bool IsEmpty => HexString.Length == 0;

        public HexString HexString { get; }

        public ReadOnlyCollection<bool> Bits { get; }

        public int BitCount { get; }

        public static bool Matches(byte[] bytes, string hexMask)
        {
            return new HexMask(hexMask).Matches(bytes);
        }

        public static bool Matches(ReadOnlySpan<byte> bytes, string hexMask)
        {
            return new HexMask(hexMask).Matches(bytes);
        }

        public bool Matches(byte value)
        {
            return Matches(BitConverter.GetBytes(value));
        }

        public bool Matches(short value)
        {
            return Matches(BitConverter.GetBytes(value));
        }

        public bool Matches(int value)
        {
            return Matches(BitConverter.GetBytes(value));
        }

        public bool Matches(long value)
        {
            return Matches(BitConverter.GetBytes(value));
        }

        public bool Matches(float value)
        {
            return Matches(BitConverter.GetBytes(value));
        }

        public bool Matches(double value)
        {
            return Matches(BitConverter.GetBytes(value));
        }

        public bool MatchesHex(string hex)
        {
            return Matches(HexStringParser.Parse(hex));
        }

        public bool Matches(ReadOnlySpan<byte> bytes)
        {
            return Matches(bytes.ToArray());
        }

        public bool Matches(byte[] bytes)
        {
            return Matches(new BitArray(bytes));
        }

        public bool Matches(BitArray bits)
        {
            if (IsEmpty)
                return true;

            if (GetBitCount(bits) < BitCount)
                return false;

            for (int i = 0; i < BitCount; i++)
            {
                if (Bits[i] && !bits[i])
                    return false;
            }

            return true;
        }

        private static int GetBitCount(BitArray bits)
        {
            for (int i = bits.Count - 1; i > 0; i--)
            {
                if (bits[i])
                    return i + 1;
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is HexMask mask && Equals(mask);
        }

        public bool Equals(HexMask other)
        {
            return HexString.Equals(other.HexString);
        }

        public override int GetHashCode()
        {
            return -1954867781 + HexString.GetHashCode();
        }

        public static bool operator ==(HexMask left, HexMask right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexMask left, HexMask right)
        {
            return !(left == right);
        }
    }
}
