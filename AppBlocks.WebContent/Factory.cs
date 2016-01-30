using System;
using AppBlocks.Cache;

namespace AppBlocks.WebContent
{
    public static class Factory
    {
        private static ICache _cache;
        private static IWebContent _webContent;

        public static string Namespace => typeof(Factory).Module.Name.Replace(".dll","");

        public static ICache Cache => _cache ?? (_cache = AppBlocks.Cache.Factory.GetDefault());
        
        public static IWebContent GetDefault()
        {
            //if (_cache != null) return _cache;
            if (_webContent != null) return _webContent;

            IWebContent results = null;
            var typeName = "AppBlocks.WebContent.WebClient.WebClientHelpers,AppBlocks.WebContent.WebClient";
            var type = Type.GetType(typeName);

            if (type != null)
            {
                results = (IWebContent)Activator.CreateInstance(type, Cache);
                //_cache = results;
                Cache.AddCache(Namespace, results);
            }

            return results;
        }
    }
}
