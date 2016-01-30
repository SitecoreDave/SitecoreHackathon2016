using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.Settings.Tests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void SettingsPropertyTest()
        {
            var results = Factory.Settings;
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetDefaultTest()
        {
            var results = Factory.GetDefault();
            Assert.IsNotNull(results);
        }
        
        [TestMethod]
        public void CachePropertyTest()
        {
            var results = Factory.Cache;
            Assert.IsNotNull(results);
        }
    }
}