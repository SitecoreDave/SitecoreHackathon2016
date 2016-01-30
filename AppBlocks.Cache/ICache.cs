namespace AppBlocks.Cache
{
    public interface ICache
    {
        void AddCache<T>(string key, T value, string namespacePrefix = null, int minutes = 60) where T : class;

        string GetCache(string key, string namespacePrefix = null);

        T GetCache<T>(string key, string namespacePrefix = null) where T : class;
    }
}
