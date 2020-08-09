using MimeTypes;
using NeocitiesApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NeocitiesApi
{
    public class NeocitiesApiClient
    {
        private readonly HttpClient _httpClient;

        public NeocitiesApiClient(string apiKey)
        {
            _httpClient = CreateHttpClient(apiKey);
        }

        public NeocitiesApiClient(string username, string password)
        {
            _httpClient = CreateHttpClient(username, password);
        }

        /// <summary>
        /// Retrieves the list of all files and folders associated with the website. 
        /// If a folder name is provided as an argument, a list of files and folders
        /// in the specified folder will be retrieved.
        /// </summary>
        /// <param name="remotePath">A specific folder on the website backend</param>
        /// <returns>A <see cref="NeocitiesFileList"/> object containing the files</returns>
        public async Task<NeocitiesFileList> GetWebsiteFileListAsync(string remotePath = "")
        {
            string urlParams;

            if (!string.IsNullOrWhiteSpace(remotePath))
            {
                urlParams = $"list?path={remotePath}";
            }
            else
            {
                urlParams = "list";
            }

            return await SendHttpGetRequestAsync<NeocitiesFileList>(urlParams);
        }

        /// <summary>
        /// Retrieves metadata about a specified website. If no argument is provided,
        /// the metadata about the website associated with the API key will be retrieved.
        /// </summary>
        /// <param name="siteName">A specific site (i.e., the "foobar" from "foobar.neocities.org")</param>
        /// <returns>A <see cref="NeocitiesWebsiteInfo"/> object containing the site's metadata</returns>
        public async Task<NeocitiesWebsiteInfo> GetWebsiteMetaDataAsync(string siteName = "")
        {
            string urlParams;

            if (!string.IsNullOrWhiteSpace(siteName))
            {
                urlParams = $"info?sitename={siteName}";
            }
            else
            {
                urlParams = "info";
            }

            return await SendHttpGetRequestAsync<NeocitiesWebsiteInfo>(urlParams);
        }

        /// <summary>
        /// Retrieves the API key for the user's current site if the user provides the username
        /// and password in order to use the Neocities API. The API will generate an API key
        /// if this request is made and the website associated with the account doesn't have one.
        /// </summary>
        /// <returns>A <see cref="NeocitiesWebsiteApiKey"/> object containing the key</returns>
        public async Task<NeocitiesWebsiteApiKey> GetWebsiteApiKeyAsync()
        {
            return await SendHttpGetRequestAsync<NeocitiesWebsiteApiKey>("key");
        }

        /// <summary>
        /// Uploads a specified file from the local computer to the user's website
        /// </summary>
        /// <param name="filePathOnDisk">The complete path to the file on disk</param>
        /// <returns><see cref="true"/> if the upload was successful, <see cref="false"/> otherwise</returns>
        public async Task<bool> UploadFileToWebsiteAsync(string filePathOnDisk)
        {
            if (filePathOnDisk.IsDirectory())
            {
                return await UploadFilesInDirectory(filePathOnDisk);
            }
            else
            {
                return await UploadFile(filePathOnDisk);
            }
        }

        /// <summary>
        /// Deletes a file or number of files from the website
        /// </summary>
        /// <param name="files">A singular filename with extension or collection of filenames with extensions</param>
        /// <returns><see cref="true"/> if the deletion was successful, <see cref="false"/> otherwise</returns>
        public async Task<bool> DeleteFilesFromWebsiteAsync(params string[] files)
        {
            var fileContent = new FormUrlEncodedContent(files.Select(file => new KeyValuePair<string, string>("filenames[]", file)));

            var deleteResult = await _httpClient.PostAsync($"delete", fileContent);

            if (!deleteResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<NeocitiesError>(await deleteResult.Content.ReadAsStringAsync());
                Console.WriteLine(error.Message);
                return deleteResult.IsSuccessStatusCode;
            }

            return deleteResult.IsSuccessStatusCode;
        }

        /// <summary>
        /// Makes the request to the Neocities API
        /// </summary>
        /// <typeparam name="T">The data model expected from the API</typeparam>
        /// <param name="urlParams"></param>
        /// <returns>The data model built from the API request</returns>
        private async Task<T> SendHttpGetRequestAsync<T>(string urlParams)
        {
            T responseModel;

            var response = await _httpClient.GetAsync(urlParams);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                throw new WebException($"Query failed! Response status code: {response.StatusCode}");
            }

            return responseModel;
        }

        /// <summary>
        /// Creates a new http client for communicating with the Neocities API with the website's API key
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

        /// <summary>
        /// Creates a new http client for communicating with the Neocities API with the user's account
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>A <see cref="HttpClient"/> configured to work with the Neocities API</returns>
        private HttpClient CreateHttpClient(string username, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"https://{username}:{password}@neocities.org/api/");

            return client;
        }

        private async Task<bool> UploadFilesInDirectory(string dir)
        {
            DirectoryInfo directory = new DirectoryInfo(dir);

            if (!directory.Exists)
            {
                Console.WriteLine($"Upload failed! The directory at '{dir}' does not exist.");
                return false;
            }

            foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
            {
                await UploadFile(file.FullName, dir);
            }


            return true;
        }

        /// <summary>
        /// Encapsulates the steps for uploading a single file to the Neocities API
        /// </summary>
        /// <param name="filePath">The path to the file on disk</param>
        /// <param name="originalDirectory">Used for uploading files from directories</param>
        /// <returns><see cref="true"/> if the upload was successful, <see cref="false"/> otherwise</returns>
        private async Task<bool> UploadFile(string filePath, string originalDirectory = "")
        {
            FileInfo fileToUpload = new FileInfo(filePath);

            if (!fileToUpload.Exists)
            {
                Console.WriteLine($"Upload failed! The file at '{filePath}' does not exist.");
                return false;
            }

            var uploadContent = CreateUploadObject(fileToUpload, originalDirectory);

            var uploadResult = await _httpClient.PostAsync("upload", uploadContent);

            return uploadResult.IsSuccessStatusCode;
        }

        /// <summary>
        /// Creates a <see cref="MultipartFormDataContent"/> object used for uploading a file from
        /// the disk to the Neocities API
        /// </summary>
        /// <param name="file">The file from disk</param>
        /// <param name="directoryName">Used for uploading files from directories so the file is placed
        /// in the correct folder in the Neocities root</param>
        /// <returns>The file as a <see cref="MultipartFormDataContent"/> object</returns>
        private MultipartFormDataContent CreateUploadObject(FileInfo file, string directoryName = "")
        {
            var fileContent = new StreamContent(file.OpenRead())
            {
                Headers =
                {
                    ContentLength = file.Length,
                    ContentType = new MediaTypeHeaderValue(MimeTypeMap.GetMimeType(file.Extension))
                }
            };

            var uploadContent = new MultipartFormDataContent();

            if (!string.IsNullOrWhiteSpace(directoryName))
            {
                var remotePath = Path.GetRelativePath(directoryName, file.FullName).ConvertWindowsDirectorySeparatorToUnixSeparator();
                uploadContent.Add(fileContent, remotePath, file.FullName);
            }
            else
            {
                uploadContent.Add(fileContent, file.Name, file.FullName);
            }
            

            return uploadContent;
        }
    }
}
