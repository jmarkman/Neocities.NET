using NeocitiesApi;
using NeocitiesApi.Models;
using NeocitiesNET.AccountInteraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeocitiesNET.ApiInteraction
{
    public class ApiCommands
    {
        private readonly NeocitiesApiClient _apiClient;

        public ApiCommands(Account account)
        {
            if (!string.IsNullOrWhiteSpace(account.Password))
            {
                _apiClient = new NeocitiesApiClient(account.Username, account.Password);
            }
            else
            {
                _apiClient = new NeocitiesApiClient(account.ApiKey);
            }

        }

        /// <summary>
        /// Lists all the files on the website associated with the
        /// current account. If a remote directory on the website is
        /// provided, this method will list all the files in that
        /// directory
        /// </summary>
        /// <param name="renderRawJson">If true, will print the raw JSON</param>
        /// <param name="remoteDirectory">The rmemote directory to list</param>
        public async Task ListAllFiles(bool renderRawJson, string remoteDirectory = "")
        {
            NeocitiesFileList filesResponse;

            if (!string.IsNullOrWhiteSpace(remoteDirectory))
            {
                filesResponse = await _apiClient.GetWebsiteFileListAsync(remoteDirectory);
            }
            else
            {
                filesResponse = await _apiClient.GetWebsiteFileListAsync();
            }

            if (filesResponse.Result == "success")
            {
                if (renderRawJson)
                {
                    RenderRawJson(filesResponse);
                }
                else
                {
                    RenderFileList(filesResponse.Files);
                }
            }

            return;
        }

        /// <summary>
        /// Prints metadata about a Neocities website. By default this method
        /// will retrieve metadata about the website associated with the current
        /// account. If a website name is provided as an argument, this method
        /// will retrieve the metadata about that website
        /// </summary>
        /// <param name="renderRawJson">If true, will print the raw JSON</param>
        /// <param name="websiteName">If provided, will retrieve the metadata of this site</param>
        public async Task GetSiteData(bool renderRawJson, string websiteName = "")
        {
            NeocitiesWebsiteInfo websiteData;

            if (!string.IsNullOrWhiteSpace(websiteName))
            {
                websiteData = await _apiClient.GetWebsiteMetaDataAsync();
            }
            else
            {
                websiteData = await _apiClient.GetWebsiteMetaDataAsync(websiteName);
            }

            if (websiteData.Result == "success")
            {
                if (renderRawJson)
                {
                    RenderRawJson(websiteData);
                }
                else
                {
                    RenderSiteData(websiteData);
                }
            }

            return;
        }

        /// <summary>
        /// Gets the API key for the user's site if they're using a
        /// username/password combo to access the API
        /// </summary>
        /// <param name="renderRawJson">If true, will print the raw JSON</param>
        public async Task GetSiteKey(bool renderRawJson)
        {
            var keyData = await _apiClient.GetWebsiteApiKeyAsync();

            if (keyData.Result == "success")
            {
                if (renderRawJson)
                {
                    RenderRawJson(keyData);
                }
                else
                {
                    Console.WriteLine($"API key: {keyData.ApiKey}");
                }
            }

            return;
        }

        /// <summary>
        /// Upload a file to the website associated with the current account
        /// </summary>
        /// <param name="filepath">The path on disk to the file to upload</param>
        /// <returns><see cref="true"/> if the upload succeeded, <see cref="false"/> otherwise</returns>
        public async Task<bool> Upload(string filepath)
        {
            if (filepath.IsDirectory())
            {
                Console.WriteLine($"Uploading files and folders in '{filepath}'");
            }
            else
            {
                Console.WriteLine($"Uploading '{filepath}'");
            }

            return await _apiClient.UploadToWebsiteAsync(filepath);
        }

        /// <summary>
        /// Delete a file or files on the website associated with the current account
        /// </summary>
        /// <param name="files">The file or collection of files to delete</param>
        /// <returns><see cref="true"/> if the deletion succeeded, <see cref="false"/> otherwise</returns>
        public async Task<bool> Delete(IEnumerable<string> files)
        {
            Console.WriteLine($"Deleting the following files: {string.Join(",", files)}");

            return await _apiClient.DeleteFromWebsiteAsync(files.ToArray());
        }

        /// <summary>
        /// If the user wants to get the output as raw JSON, this method will serialize the 
        /// Neocities model to a JSON string and print the serialized JSON to the console 
        /// </summary>
        /// <param name="jsonResponse">The response object that came back from the query</param>
        private void RenderRawJson(NeocitiesWebsiteBase jsonResponse)
        {
            if (jsonResponse is NeocitiesFileList fileList)
            {
                foreach (var file in fileList.Files)
                {
                    var fileJson = JsonConvert.SerializeObject(file, Formatting.Indented);
                    Console.WriteLine(fileJson);
                }
            }
            else
            {
                var json = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
                Console.WriteLine(json);
            }
        }

        /// <summary>
        /// Prints each file returned from the all files query to the console window
        /// in an organized manner
        /// </summary>
        /// <param name="allFiles">The list of files from the response object</param>
        private void RenderFileList(List<NeocitiesFile> allFiles)
        {
            StringBuilder listBuilder = new StringBuilder();

            foreach (var file in allFiles)
            {
                listBuilder.AppendLine($"File: {file.Path}");
                listBuilder.AppendLine($"Directory?: {file.IsDirectory}");
                listBuilder.AppendLine($"Size: {Convert.ToDouble(file.Size).ConvertFromBytesToBase10Kilobytes()} kB");
                listBuilder.AppendLine($"Last updated: {file.UpdatedAt.ToString("f", CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.ToString()))}");
                listBuilder.AppendLine($"SHA-1 Hash: {file.SHA1Hash}");
                listBuilder.AppendLine();
            }

            Console.WriteLine(listBuilder.ToString());
        }

        /// <summary>
        /// Prints the metadata associated with the website to the console window
        /// in an organized manner
        /// </summary>
        /// <param name="websiteData">The response object containing the site metadata</param>
        private void RenderSiteData(NeocitiesWebsiteInfo websiteData)
        {
            StringBuilder metadataBuilder = new StringBuilder();

            metadataBuilder.AppendLine($"Website Name: {websiteData.Attributes.SiteName}");
            metadataBuilder.AppendLine($"Number of website hits: {websiteData.Attributes.NumberOfHits}");
            metadataBuilder.AppendLine($"Site Creation Date: {websiteData.Attributes.CreatedAt.ToString("f", CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.ToString()))}");
            metadataBuilder.AppendLine($"Site Last Updated: {websiteData.Attributes.LastUpdated.ToString("f", CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.ToString()))}");
            metadataBuilder.AppendLine($"Domain: {websiteData.Attributes.Domain}");
            metadataBuilder.Append("Website Tags: ");
            metadataBuilder.AppendJoin(", ", websiteData.Attributes.Tags);

            Console.WriteLine(metadataBuilder.ToString());
        }
    }
}
