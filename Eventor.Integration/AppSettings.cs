using System.Configuration;

namespace Eventor.Integration
{
    public class AppSettings : IProvideEventorConfiguration
    {
        private readonly string _keySuffix = string.Empty;

        public AppSettings()
        {
            
        }

        public AppSettings(string keySuffix)
        {
            _keySuffix = keySuffix;
        }

        private string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string OrgId { get { return Get("OrgId" + _keySuffix); } }
        public string EventorBaseUrl { get { return Get("EventorBaseUrl" + _keySuffix); } }
        public string ApiKey { get { return Get("ApiKey" + _keySuffix); } }
    }
}
