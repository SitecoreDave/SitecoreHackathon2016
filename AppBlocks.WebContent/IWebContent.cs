using System.Collections.Specialized;

namespace AppBlocks.WebContent
{
    public interface IWebContent
    {
        string GetResults(string url, NameValueCollection headers = null, int cacheMinutes = 0);
    
        T GetResults<T>(string url, NameValueCollection headers = null, int cacheMinutes = 0) where T : class;
    }
}