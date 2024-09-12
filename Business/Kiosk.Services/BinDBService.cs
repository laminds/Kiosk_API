using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Services
{
    public class BinDBService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _url = "https://pci.bindb.com/api/iin_json/";

        public BinDBService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<JObject> LookupBin(string binNumber)
        {
            var Url = $"{_url}?api_key={_apiKey}&bin={binNumber}";
            var response = await _httpClient.GetAsync(Url);
            var Status = response.StatusCode;
            if (Status == HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            else
            {
                return null;
            }
        }
    }
}
