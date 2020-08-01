using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NeocitiesApi
{
    public class NeocitiesApi
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public NeocitiesApi(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = CreateHttpClient(apiKey);
        }

        public 

        /// <summary>
        /// Creates a new http client for communicating with the Neocities API
        /// </summary>
        /// <param name="apiKey">The site's API key</param>
        /// <returns>A <see cref="HttpClient"/> configured to work with the Neocities API</returns>
        private HttpClient CreateHttpClient(string apiKey)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://neocities.org/api");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            return client;
        }
    }
}
