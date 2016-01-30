using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.Cache.HttpCache.Tests
{
    [TestClass]
    public class CacheHelpersTests
    {
        public static string Name => typeof(CacheHelpersTests).Name;

        private readonly CacheHelpers _cache = new CacheHelpers();

        [TestMethod]
        public void AddCacheTestWithNullParameters()
        {
            _cache.AddCache<string>(null, null);
        }

        [TestMethod]
        public void AddCacheTestWithNullValue()
        {
            _cache.AddCache<string>("AddCacheTestWithNullValue", null);
        }
        
        [TestMethod]
        public void AddCacheTestStringReturnsValue()
        {
            var key = "AddCacheTestWithNullValue";
            var value = "test";
            _cache.AddCache(key, value);
            var confirmValue = _cache.GetCache(key);
            Assert.AreEqual(value, confirmValue);
        }
        
        [TestMethod]
        public void AddCacheWithNamespaceTestStringReturnsValue()
        {
            var key = "AddCacheTestWithNullValue";
            var value = "test";
            _cache.AddCache(key, value, Name);
            var results = _cache.GetCache(key, Name);
            Assert.AreEqual(value, results);
        }
        
        //[TestMethod]
        //public void AddCacheTestIntReturnsValue()
        //{
        //    var key = "AddCacheTestWithNullValue";
        //    var value = 1;
        //    _cache.AddCache(key, value);
        //    var confirmValue = _cache.GetCache(key);
        //    Assert.AreEqual(value, confirmValue);
        //}
        
        //public void AddCacheWithNamespaceTestIntReturnsValue()
        //{
        //    var key = "AddCacheTestWithNullValue";
        //    var value = 1;
        //    _cache.AddCache(key, value, 60, Name);
        //    var confirmValue = _cache.GetCache(key, Name);
        //    Assert.AreEqual(value, confirmValue);
        //}
    }
}