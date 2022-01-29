using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HexMask.Tests
{
    [TestClass]
    public class HexMaxTests
    {
        [TestMethod]
        public void DefaultHexMask()
        {
            var hm = new HexMask();
            Assert.AreEqual(default, hm.HexString);
            Assert.IsTrue(hm.IsEmpty);
        }

        [TestMethod]
        public void DefaultHexMaskMatches()
        {
            var hm = new HexMask();
            Assert.IsTrue(hm.Matches(new byte[] { 123, 255 }));
            Assert.IsTrue(hm.Matches(new byte[] { 255, 255 }));
            Assert.IsTrue(hm.Matches(new byte[] { 123, 123 }));
        }

        [TestMethod]
        public void HexMaskMatchesBytes()
        {
            var hm = new HexMask("12ff");
            Assert.IsTrue(hm.Matches(new byte[] { 0x12, 0xff }));
            Assert.IsTrue(hm.Matches(new byte[] { 0x16, 0xff }));
            Assert.IsTrue(hm.Matches(new byte[] { 0x33, 0xff, 0x1 }));
            Assert.IsTrue(hm.Matches(new byte[] { 0xff, 0xff, 0x1 }));
        }

        [TestMethod]
        public void HexMaskDoesntMatchBytes()
        {
            var hm = new HexMask("12ff");
            Assert.IsFalse(hm.Matches(new byte[] { 0x12, 0x7B }));
            Assert.IsFalse(hm.Matches(new byte[] { 0xff, 0x7B }));
            Assert.IsFalse(hm.Matches(new byte[] { 0x31, 0xff }));
            Assert.IsFalse(hm.Matches(new byte[] { 0x10, 0xff, 0x1 }));
        }

        [TestMethod]
        public void HexMaskDoesntMatchBytes_Short()
        {
            var hm = new HexMask("12ff");
            Assert.IsFalse(hm.Matches(new byte[] { 0x12 }));
            Assert.IsFalse(hm.Matches(new byte[] { 0xff }));
            Assert.IsFalse(hm.Matches(new byte[] { 0x31 }));
            Assert.IsFalse(hm.Matches(new byte[] { 0x10 }));
        }

        [TestMethod]
        public void HexMaskMatchesInteger()
        {
            var hm = new HexMask("ff00");
            Assert.IsTrue(hm.Matches(0xff));
            Assert.IsTrue(hm.Matches(0x12ff));
            Assert.IsTrue(hm.Matches(0xffff));
        }

        [TestMethod]
        public void HexMaskDoesntMatchInteger()
        {
            var hm = new HexMask("ff00");
            Assert.IsFalse(hm.Matches(0xff00));
            Assert.IsFalse(hm.Matches(0xff12));
            Assert.IsFalse(hm.Matches(0x12));
        }

        [TestMethod]
        public void StaticMatchesByteArray()
        {
            byte[] vs = new byte[] { 255, 0x12 };
            Assert.IsTrue(HexMask.Matches(vs, "FF00"));
        }
    }
}