using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UTP
{
    public interface IFacebookClient
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string agrs = null);
        Task PostAsync(string accessToken, string endPoint, object data, string args = null);
    }
    class FacebookClient : IFacebookClient
    {
        private readonly HttpClient _httpClient;
        public FacebookClient()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://graph.facebook.com/v3.1/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync(endpoint+"?access_token="+accessToken+"&"+args);
            if(!response.IsSuccessStatusCode)
            {
                return default(T);
            }
            var results = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(results);
        }

        public async Task PostAsync(string accessToken, string endPoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            await _httpClient.PostAsync(endPoint+"?access_token="+accessToken+"&"+args, payload);
        }

        public static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
