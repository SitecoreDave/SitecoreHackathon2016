using System.Collections.Specialized;
using System.Linq;
using AppBlocks.Cache;
using AppBlocks.Settings;
using AppBlocks.WebContent;
using Newtonsoft.Json.Linq;

namespace AppBlocks.FullContact
{
    /// <summary>
    /// FullContact
    /// https://www.fullcontact.com/developer/docs/
    /// </summary>
    public class Api
    {
        private static string Namespace => typeof(Api).Module.Name.Replace(".dll", "");

        private static ICache _cache;
        private static ISettings _settings;
        private static IWebContent _webContent;
        private static int _counter = 0;
        public static ICache Cache => _cache ?? (_cache = AppBlocks.Cache.Factory.GetDefault());
        public static ISettings Settings => _settings ?? (_settings = AppBlocks.Settings.Factory.GetDefault());
        public static IWebContent WebContent => _webContent ?? (_webContent = AppBlocks.WebContent.Factory.GetDefault());

        public Api()
        {
        }

        public Api(ICache cache, ISettings settings, IWebContent webContent)
        {
            _cache = cache;
            _settings = settings;
            _webContent = webContent;
        }

        public NameValueCollection GetInformation(string emailAddress = null)
        {
            if (string.IsNullOrEmpty(emailAddress)) return null;

            var cacheKey = emailAddress + ".data";

            var result = Cache.GetCache<NameValueCollection>(cacheKey);

            if (result != null) return result;

            var data = GetInformationJson(emailAddress);

            //if (results.StartsWith("Error"))

            if (data == null) return null;
            
            result = new NameValueCollection();

            var contactInfo = data["contactInfo"];
            if (contactInfo != null)
            {
                if (contactInfo["familyName"] != null)
                {
                    result["lastname"] = contactInfo["familyName"].ToString();
                }
                if (contactInfo["givenName"] != null)
                {
                    result["firstname"] = contactInfo["givenName"].ToString();
                }
                if (contactInfo["websites"] != null)
                {
                    _counter = 0;

                    foreach (var website in contactInfo["websites"])
                    {
                        _counter++;
                        result["website" + _counter] = website["url"].ToString();
                    }
                }
            }


            var demographics = data["demographics"];
            if (demographics["gender"] != null)
            {
                result["gender"] = demographics["gender"].ToString();
            }

            var location = demographics["locationDeduced"];

            if (location?["city"]?["name"] != null)
            {
                result["city"] = location["city"]["name"].ToString();
            }

            if (location?["county"]?["name"] != null)
            {
                result["county"] = location["county"]["name"].ToString();
            }

            if (location?["state"]?["name"] != null)
            {
                result["state"] = location["state"]["name"].ToString();
            }

            if (location?["continent"]?["name"] != null)
            {
                result["continent"] = location["continent"]["name"].ToString();
            }

            if (location?["country"]?["name"] != null)
            {
                result["country"] = location["country"]["name"].ToString();
            }
            
            var organizations = data["organizations"];
            if( organizations != null)
            {
                _counter = 0;
                foreach (
                    var organization in
                        organizations.Where(
                            org => org["name"] != null && org["title"] != null && org["startDate"] != null))
                {
                    if (organization["isPrimary"] != null && organization["isPrimary"].ToString() == "true")
                    {
                        result["jobtitle"] = organization["title"].ToString();
                    }
                    _counter++;
                    result["org" + _counter] = organization["name"] + "|" + organization["title"] + "|" +
                                            organization["startDate"] + "|" +
                                            organization["current"];
                }
            }

            _counter = 0;
            foreach (var photo in data["photos"].Where(photo => photo["type"] != null && photo["url"] != null))
            {
                _counter++;
                result["photo" + _counter] = photo["type"] + "|" + photo["url"] + "|" + photo["isPrimary"];
            }

            _counter = 0;
            foreach (var socialProfile in data["socialProfiles"])
            {
                _counter++;
                result["socialProfile" + _counter] = socialProfile["typeName"] + "|" + socialProfile["url"];
            }

            if (data["digitalFootprint"] != null)
            {
                _counter = 0;
                foreach (var topic in data["digitalFootprint"]["topics"])
                {
                    _counter++;
                    result["topic" + _counter] = topic["provider"] + "|" + topic["value"];
                }
            }

            Cache.AddCache(cacheKey, result);

            return result;
        }

        public JObject GetInformationJson(string emailAddress = null)
        {
            if (string.IsNullOrEmpty(emailAddress)) return null;

            var cacheKey = emailAddress + ".json";

            var result = Cache.GetCache<JObject>(cacheKey);

            if (result != null) return result;

            var apiUrl = Settings.GetSetting("ApiUrl", "", Namespace);
            var apiId = Settings.GetSetting("apiId", "", Namespace);

            if (string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(apiId)) return null;

            var fullUrl = string.Format(apiUrl, apiId, emailAddress);

            result = WebContent.GetResults<JObject>(fullUrl);

            //if (results.StartsWith("Error"))

            Cache.AddCache(cacheKey, result);

            return result;
        }
    }
}
