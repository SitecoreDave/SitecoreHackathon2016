using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.Extensions.TypeExtensions.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void ChangeTypeNullTest()
        {
            string value = null;
            var results = value.ChangeType<int>();
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ChangeTypeDateTimeInvalidTest()
        {
            const string value = "1";
            var results = value.ChangeType<DateTime>();
            Assert.AreEqual(results, default(DateTime));
        }

        [TestMethod]
        public void ChangeTypeStringToIntTest()
        {
            const int value = 0;
            var results = value.ChangeType<int>();
            Assert.AreEqual(value, results);
        }
    }
}