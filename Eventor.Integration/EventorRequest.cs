using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace Eventor.Integration
{
    public class EventorRequest
    {
        private readonly string _query;
        private string _baseUrl;
        private string _apiKey;
        private string _orgId;

        public EventorRequest(IProvideEventorConfiguration configuration)
        {
            _baseUrl = configuration.EventorBaseUrl;
            _apiKey = configuration.ApiKey;
            _orgId = configuration.OrgId;

            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
        }

        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public string GetXml(string query)
        {
            query = query.Replace("{OrgId}", _orgId);
            query = query.Replace("**ORGID**", _orgId);

            var uri = _baseUrl + "/api/" + query;
            var webRequest = WebRequest.Create(uri);
            webRequest.Headers.Add("ApiKey", _apiKey);

            var response = webRequest.GetResponse();

            string responseText = null;

            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;
        }
    }

    public interface IProvideEventorConfiguration
    {
        string OrgId { get; }
        string EventorBaseUrl { get; }
        string ApiKey { get; }
    }
}
