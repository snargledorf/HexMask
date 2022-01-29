using System;
using System.Collections.Generic;

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
        }

        public bool IsEmpty => HexString.Length == 0;

        public HexString HexString { get; }

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

        public bool Matches(byte[] bytes)
        {
            return Matches(bytes.AsSpan());
        }

        public bool Matches(ReadOnlySpan<byte> bytes)
        {
            if (IsEmpty)
                return true;

            if (bytes.Length < HexString.ByteCount)
                return false;

            for (int i = 0; i < HexString.ByteCount; i++)
            {
                byte hexByte = HexString[i];
                if ((hexByte & bytes[i]) != hexByte)
                    return false;
            }

            return true;
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
