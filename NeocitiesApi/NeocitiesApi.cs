using NeocitiesApi.Models;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

        /// <summary>
        /// Retrieves the list of all files and folders associated with the website. 
        /// If a folder name is provided as an argument, a list of files and folders
        /// in the specified folder will be retrieved.
        /// </summary>
        /// <param name="remotePath">A specific folder on the website backend</param>
        /// <returns>A <see cref="WebsiteFileList"/> object containing the files</returns>
        public async Task<WebsiteFileList> GetWebsiteFileListAsync(string remotePath = "")
        {
            WebsiteFileList fileList;
            string urlParams;

            if (!string.IsNullOrWhiteSpace(remotePath))
            {
                urlParams = $"list?path={remotePath}";
            }
            else
            {
                urlParams = "list";
            }

            var response = await _httpClient.GetAsync(urlParams);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                fileList = JsonConvert.DeserializeObject<WebsiteFileList>(data);
            }
            else
            {
                throw new WebException("Could not retrieve the list of files for the website");
            }

            return fileList;
        }

        /// <summary>
        /// Retrieves metadata about a specified website. If no argument is provided,
        /// the metadata about the website associated with the API key will be retrieved.
        /// </summary>
        /// <param name="siteName">A specific site (i.e., the "foobar" from "foobar.neocities.org")</param>
        /// <returns>A <see cref="WebsiteInfo"/> object containing the site's metadata</returns>
        public async Task<WebsiteInfo> GetWebsiteMetaDataAsync(string siteName = "")
        {
            WebsiteInfo info;
            string urlParams;

            if (!string.IsNullOrWhiteSpace(siteName))
            {
                urlParams = $"info?sitename={siteName}";
            }
            else
            {
                urlParams = "info";
            }

            var response = await _httpClient.GetAsync(urlParams);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                info = JsonConvert.DeserializeObject<WebsiteInfo>(data);
            }
            else
            {
                throw new WebException("Could not retrieve the metadata for the website");
            }

            return info;
        }

        /// <summary>
        /// Creates a new http client for communicating with the Neocities API
        /// </summary>
        /// <param name="apiKey">The site's API key</param>
        /// <returns>A <see cref="HttpClient"/> configured to work with the Neocities API</returns>
        private HttpClient CreateHttpClient(string apiKey)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://neocities.org/api/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            return client;
        }
    }
}
