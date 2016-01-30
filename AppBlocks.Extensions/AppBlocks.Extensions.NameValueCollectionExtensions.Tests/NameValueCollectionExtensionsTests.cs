using System;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.Extensions.NameValueCollectionExtensions.Tests
{
    [TestClass]
    public class NameValueCollectionExtensionsTests
    {
        readonly NameValueCollection _data = new NameValueCollection {{"key1", "0"}, {"key2", "1"}};

        [TestMethod]
        public void ChangeValueTypeStringToIntNullTest()
        {
            var results = _data.ChangeValueType<int>("key3");
            Assert.AreEqual(results, default(int));
        }

        [TestMethod]
        public void ChangeValueTypeStringToDateTimeInvalidTest()
        {
            var results = _data.ChangeValueType<DateTime>("key1");
            Assert.AreEqual(results, default(DateTime));
        }

        [TestMethod]
        public void ChangeValueTypeStringToIntTest()
        {
            var results = _data.ChangeValueType<int>("key1");
            Assert.AreEqual(0, results);

            results = _data.ChangeValueType<int>("key2");
            Assert.AreEqual(1, results);
        }

        
        [TestMethod]
        public void ToKeyValueStringTest()
        {
            var results = _data.ToKeyValueString();
            Assert.AreEqual("key1=0\r\nkey2=1\r\n", results);

            results = _data.ToKeyValueString("=", "|");
            Assert.AreEqual("key1=0|key2=1|", results);
        }
    }
}