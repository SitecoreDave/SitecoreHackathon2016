using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.FullContact.Tests
{
    [TestClass]
    public class ApiTests
    {
        //private const string TestEmail = "david@davidlwalker.com";
        private const string TestEmail = "ronney@ronneymichelle.com";

        [TestMethod]
        public void GetInformationNoParameterTest()
        {
            var api = new Api();
            var results = api.GetInformation();
            Assert.IsNull(results);
        }

        [TestMethod]
        public void GetInformationTest()
        {
            var api = new Api();
            var results = api.GetInformation(TestEmail);
            Assert.IsNotNull(results);
        }

        // No need to test, since it is called from above
        //[TestMethod]
        //public void GetInformationJsonNoParameterTest()
        //{
        //    var api = new Api();
        //    var results = api.GetInformationJson();
        //    Assert.IsNull(results);
        //}

        //[TestMethod]
        //public void GetInformationJsonTest()
        //{
        //    var api = new Api();
        //    var results = api.GetInformationJson(TestEmail);
        //    Assert.IsNotNull(results);
        //}
    }
}