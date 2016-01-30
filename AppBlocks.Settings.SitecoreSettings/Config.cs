using AppBlocks.Cache;

namespace AppBlocks.Settings.SitecoreSettings
{
    public class Config : ISettings
    {
        private readonly ICache _cache;
        
        public Config(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Get Settings from Sitecore Config file(s)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="namespacePrefix"></param>
        /// <returns></returns>
        public string GetSetting(string name, string defaultValue = null, string namespacePrefix = null)
        {
            if (!string.IsNullOrEmpty(namespacePrefix)) name = namespacePrefix + "." + name;
            var value = _cache.GetCache(name);
            if(string.IsNullOrEmpty(value)) value = Sitecore.Configuration.Settings.GetSetting(name, defaultValue);
            _cache?.AddCache(name, value);
            return value;
        }
    }
}