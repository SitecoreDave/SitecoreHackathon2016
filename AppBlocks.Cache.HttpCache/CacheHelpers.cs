using System;
using System.Web;
using System.Web.Caching;

namespace AppBlocks.Cache.HttpCache
{
    public class CacheHelpers : ICache
    {
        public void AddCache<T>(string key, T value, string namespacePrefix = null, int minutes = 60) where T : class
        {
            if (string.IsNullOrEmpty(key)) return;

            if (!string.IsNullOrEmpty(namespacePrefix)) key = namespacePrefix + "." + key;
           
            if (value == null)
            {
                HttpRuntime.Cache.Remove(key);
            }
            else
            {
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(minutes),
                    System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);                
            }
        }

        public string GetCache(string key, string namespacePrefix = null)
        {
            return GetCache<string>(key, namespacePrefix);
        }

        public T GetCache<T>(string key, string namespacePrefix = null) where T : class
        {
            if (!string.IsNullOrEmpty(namespacePrefix)) key = namespacePrefix + "." + key;
      
            return HttpRuntime.Cache[key] != null
                ? HttpRuntime.Cache[key] as T
                : null;
        }
    }
}
