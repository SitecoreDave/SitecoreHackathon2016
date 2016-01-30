using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace AppBlocks.WebContent.WebClient.Tests
{
    [TestClass]
    public class WebClientHelpersTests
    {
        private const string JsonTestUrl = "http://scooterlabs.com/echo.json";
        //private const string JsonTestUrl = "http://echo.jsontest.com/key/value/one/two";

        [TestMethod]
        public void GetResultsNullUrlTest()
        {
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults(null);
            Assert.IsNull(results);
        }
        
        [TestMethod]
        public void GetResultsTNullUrlTest()
        {
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults<JObject>(null);
            Assert.IsNull(results);
        }

        [TestMethod]
        public void GetResultsStringTest()
        {
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults(JsonTestUrl);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Contains("client_ip"));
        }

        [TestMethod]
        public void GetResultsJsonTest()
        {
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults<JObject>(JsonTestUrl);
            
            Assert.IsNotNull(results);
            Assert.IsNotNull(results["client_ip"]);
        }
        
        [TestMethod]
        public void GetResultsJsonWithHeadersTest()
        {
            var headers = new NameValueCollection {{"test", "test"}};
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults<JObject>(JsonTestUrl, headers);
            
            Assert.IsNotNull(results);
            Assert.IsNotNull(results["client_ip"]);
        }

        [TestMethod]
        public void GetResultsBadUrlTest()
        {
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults("http://localhost/badurl");
            const string badUrlResults = "Error retrieving url:http://localhost/badurl - The remote server returned an error: (404) Not Found.";
            Assert.AreEqual(results, badUrlResults);
        }

        [TestMethod]
        public void GetResultsBadUrlJsonTest()
        {
            var cache = new Cache.HttpCache.CacheHelpers();
            var webClientHelper = new WebClientHelpers(cache);
            var results = webClientHelper.GetResults<JObject>("http://localhost/badurl");
            const string badUrlResults = "{\r\n  \"Error\": \"Error retrieving url:http://localhost/badurl - The remote server returned an error: (404) Not Found.\"\r\n}";
            Assert.AreEqual(results.ToString(), badUrlResults);
        }
    }
}