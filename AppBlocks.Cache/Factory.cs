using System;

namespace AppBlocks.Cache
{
    public static class Factory
    {
        private static ICache _cache;

        public static string Namespace => typeof(Factory).Module.Name.Replace(".dll","");

        public static ICache Cache => _cache ?? (_cache = GetDefault());
        
        public static ICache GetDefault()
        {
            if (_cache != null) return _cache;
            ICache results = null;
            var typeName = Namespace + ".HttpCache.CacheHelpers," + Namespace + ".HttpCache";
            var type = Type.GetType(typeName);

            if (type != null)
            {
                results = (ICache)Activator.CreateInstance(type);
                _cache = results;
                results.AddCache(Namespace, results);
            }

            return results;
        }
    }
}
