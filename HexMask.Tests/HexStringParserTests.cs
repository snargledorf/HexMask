using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HexMask.Tests
{
    [TestClass]
    public class HexStringParserTests
    {
        [TestMethod]
        public void HexStringToBytes()
        {
            const string hexString = "FFFF";
            var expectedBytes = new byte[2] { 255, 255 };

            byte[] result = HexStringParser.Parse(hexString);
            CollectionAssert.AreEqual(expectedBytes, result);
        }

        [TestMethod]
        public void HexStringToBytesMixedCase()
        {
            const string hexString = "FFff";
            var expectedBytes = new byte[2] { 255, 255 };

            byte[] result = HexStringParser.Parse(hexString);
            CollectionAssert.AreEqual(expectedBytes, result);
        }

        [TestMethod]
        public void HexStringToBytesShort()
        {
            const string hexString = "FFF";
            byte[] expectedBytes = new byte[2] { 255, 0xF0 };

            byte[] result = HexStringParser.Parse(hexString);

            CollectionAssert.AreEqual(expectedBytes, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HexStringToBytesInvalidString_NonHexCharacter()
        {
            const string hexString = "FFFG";
            HexStringParser.Parse(hexString);
        }
    }
}