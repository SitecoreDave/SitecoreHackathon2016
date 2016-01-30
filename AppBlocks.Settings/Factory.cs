using System;
using AppBlocks.Cache;

namespace AppBlocks.Settings
{
    public static class Factory
    {
        private static ICache _cache;
        private static ISettings _settings;

        public static string Namespace => typeof(Factory).Module.Name.Replace(".dll","");

        public static ICache Cache => _cache ?? (_cache = AppBlocks.Cache.Factory.GetDefault());
        public static ISettings Settings => _settings ?? (_settings = GetDefault());

        public static ISettings GetDefault(ICache cache = null)
        {
            if (_settings != null) return _settings;
            if (cache == null) cache = Cache;

            _settings = cache.GetCache<ISettings>(Namespace);
            if (_settings != null) return _settings;

            ISettings results = null;
            var typeName = Namespace + ".AppSettings.SettingsHelpers," + Namespace + ".AppSettings";
            var type = Type.GetType(typeName);

            if (type != null)
            {
                results = (ISettings)Activator.CreateInstance(type, Cache);
                _settings = results;
                cache.AddCache(Namespace, results);
                return results;
            }

            typeName = Namespace + ".SitecoreSettings.Config," + Namespace + ".SitecoreSettings";
            type = Type.GetType(typeName);

            if (type != null)
            {
                results = (ISettings)Activator.CreateInstance(type, cache);
                _settings = results;
                cache.AddCache(Namespace, results);
            }

            return results;
        }
    }
}