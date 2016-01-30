using System;
using System.Collections.Specialized;
using AppBlocks.Cache;
using AppBlocks.Extensions;
using Newtonsoft.Json.Linq;

namespace AppBlocks.WebContent.WebClient
{
    public class WebClientHelpers : IWebContent
    {
        private readonly ICache _cache;
        
        public WebClientHelpers(ICache cache)
        {
            _cache = cache;
        }

        public string GetResults(string url, NameValueCollection headers = null, int cacheMinutes = 0)
        {
            return GetResults<string>(url, headers, cacheMinutes);
        }

        public T GetResults<T>(string url, NameValueCollection headers = null, int cacheMinutes = 0) where T : class
        {
            if (string.IsNullOrEmpty(url)) return null;

            var cacheKey = url.Replace("http://", "").Replace("https://", "");

            var results = _cache.GetCache<T>(cacheKey);

            if (results != null) return results;

            string value;

            try
            {
                //if (headers == null || headers.Count == 0)
                //{
                //    value = Sitecore.Web.WebUtil.ExecuteWebPage(url);
                //}
                //else
                //{
                    using (var webClient = new System.Net.WebClient())
                    {
                        if (headers != null)
                        {
                            foreach (string key in headers)
                            {
                                webClient.Headers.Add(key, headers[key]);
                            }
                        }

                        value = webClient.DownloadString(url);
                    }
                //}
            }
            catch (Exception exception)
            {
                value = "Error retrieving url:" + url + " - " + exception.Message;
                if (typeof (T) == typeof (JObject)) value = "{\"Error\":\"" + value + "\"}";
            }

            try
            {
                results = typeof(T) != typeof(JObject) ? value.ChangeType<T>() : JObject.Parse(value).ChangeType<T>();
            }
            catch (Exception exception)
            {
                value = "Error converting response from url:" + url + " - " + value + exception.Message;
                if (typeof (T) == typeof (JObject)) value = "{\"Error\":\"" + value + "\"}";
            }
            _cache.AddCache(cacheKey, results);

            return results;
        }
    }
}