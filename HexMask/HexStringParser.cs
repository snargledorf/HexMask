using System;

namespace HexMask
{
    internal static class HexStringParser
    {
        internal static byte[] Parse(string hexString)
        {
            HexStringParseResult hexMaskParseResult = new HexStringParseResult();
            if (TryParse(hexString, ref hexMaskParseResult))
                return hexMaskParseResult.result;
            else
                throw GetHexMaskParseException(hexMaskParseResult);
        }

        internal static bool TryParse(string hexString, out byte[] bytes)
        {
            HexStringParseResult hexMaskParseResult = new HexStringParseResult();
            if (TryParse(hexString, ref hexMaskParseResult))
            {
                bytes = hexMaskParseResult.result;
                return true;
            }

            bytes = default;
            return false;
        }

        internal static bool TryParse(string hexString, ref HexStringParseResult result)
        {
            result = new HexStringParseResult();

            CharToHexNumberResult charToHexResult = new CharToHexNumberResult();

            int byteCount = GetByteCount(hexString);
            var bytes = new byte[byteCount];

            for (int charIndex = 0, byteIndex = 0;
                byteIndex < bytes.Length && charIndex < hexString.Length;
                charIndex += 2, byteIndex++)
            {
                if (TryCharToHexNumber(hexString[charIndex], ref charToHexResult))
                    bytes[byteIndex] = (byte)(charToHexResult.result << 4);
                else
                {
                    result.CopyFailure(charToHexResult);
                    return false;
                }

                int nextCharIndex = charIndex + 1;
                if (nextCharIndex != hexString.Length)
                {
                    if (TryCharToHexNumber(hexString[nextCharIndex], ref charToHexResult))
                        bytes[byteIndex] |= (byte)(charToHexResult.result);
                    else
                    {
                        result.CopyFailure(charToHexResult);
                        return false;
                    }
                }
            }

            result.result = bytes;
            return true;
        }

        public static int GetByteCount(string hex)
        {
            return ((hex.Length - 1) / 2) + 1;
        }

        private static bool TryCharToHexNumber(char hexDigit, ref CharToHexNumberResult result)
        {
            result = new CharToHexNumberResult();

            var b = (int)char.ToUpper(hexDigit);
            if (b < 58 && b > 47)
            {
                result.result = b - 48;
                return true;
            }

            b -= 55;
            if (b < 10 || b > 15)
            {
                result.failure = ParseFailureKind.Argument;
                result.failureArgumentName = nameof(hexDigit);
                result.failureMessage = $"'{hexDigit}' is not a valid hex digit";
                return false;
            }

            result.result = b;
            return true;
        }

        private static Exception GetHexMaskParseException(HexStringParseResult result)
        {
            switch (result.failure)
            {
                case ParseFailureKind.Argument:
                    return new ArgumentException(result.failureMessage, result.failureArgumentName);
            }

            throw new ArgumentException($"Unknown HexMask parse failure {result.failure}");
        }

        internal struct CharToHexNumberResult
        {
            internal int result;
            internal ParseFailureKind failure;
            internal string failureArgumentName;
            internal string failureMessage;
        }

        internal struct HexStringParseResult
        {
            internal byte[] result;
            internal ParseFailureKind failure;
            internal string failureArgumentName;
            internal string failureMessage;

            internal void CopyFailure(CharToHexNumberResult charToHexNumberResult)
            {
                failure = charToHexNumberResult.failure;
                failureArgumentName = charToHexNumberResult.failureArgumentName;
                failureMessage = charToHexNumberResult.failureMessage;
            }
        }

        internal enum ParseFailureKind
        {
            Argument
        }
    }
}
