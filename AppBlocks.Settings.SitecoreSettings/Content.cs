using System.Reflection;
using AppBlocks.Cache;

namespace AppBlocks.Settings.SitecoreSettings
{
    public class Content : ISettings
    {
        private readonly ICache _cache;
        
        public Content(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Get Settings from Sitecore Content Items
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="namespacePrefix"></param>
        /// <returns></returns>
        public string GetSetting(string name, string defaultValue, string namespacePrefix = null)
        {
            if (!string.IsNullOrEmpty(namespacePrefix)) name = namespacePrefix + "." + name;
            var value = _cache.GetCache(name);
            if(string.IsNullOrEmpty(value)) value = Sitecore.Configuration.Settings.GetSetting(name, defaultValue);
            _cache?.AddCache(name, value);
            return value;
        }
    }
}