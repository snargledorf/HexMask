using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HexMask.Tests
{
    [TestClass]
    public class HexStringTests
    {
        [TestMethod]
        public void HexString()
        {
            const string hexString = "FFFF";
            var expectedBytes = new byte[2] { 255, 255 };

            var result = new HexString(hexString);
            CollectionAssert.AreEqual(expectedBytes, result.ToArray());
        }

        [TestMethod]
        public void HexStringMixedCase()
        {
            const string hexString = "FFff";
            var expectedBytes = new byte[2] { 255, 255 };

            var result = new HexString(hexString);
            CollectionAssert.AreEqual(expectedBytes, result.ToArray());
        }

        [TestMethod]
        public void HexStringShort()
        {
            const string hexString = "FFF";
            byte[] expectedBytes = new byte[2] { 255, 0xF0 };

            var result = new HexString(hexString);
            CollectionAssert.AreEqual(expectedBytes, result.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HexStringInvalidString_NonHexCharacter()
        {
            const string hexString = "FFFG";
            new HexString(hexString);
        }

        [TestMethod]
        public void HexStringAlterArray()
        {
            const string hexString = "FFff";
            var expectedBytes = new byte[2] { 255, 255 };

            var result = new HexString(hexString);
            byte[] bytes = result.ToArray();
            bytes[0] = 0;
            CollectionAssert.AreNotEqual(expectedBytes, bytes);
        }
    }
}