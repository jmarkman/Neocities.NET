using NeocitiesApi;
using NeocitiesApi.Models;
using NeocitiesNET.AccountInteraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
        /// <param name="remoteDirectory">The rmemote directory to list</param>
        public async void ListAllFiles(string remoteDirectory = "")
        {
            StringBuilder listBuilder = new StringBuilder();
            NeocitiesFileList allFiles;

            if (!string.IsNullOrWhiteSpace(remoteDirectory))
            {
                allFiles = await _apiClient.GetWebsiteFileListAsync();
            }
            else
            {
                allFiles = await _apiClient.GetWebsiteFileListAsync(remoteDirectory);
            }

            if (allFiles.Result == "success")
            {
                foreach (var file in allFiles.Files)
                {
                    listBuilder.AppendLine($"File: {file.Path}");
                    listBuilder.AppendLine($"Directory?: {file.IsDirectory}");
                    listBuilder.AppendLine($"Size: {file.Size.ConvertFromBytesToBase10Kilobytes()} kB");
                    listBuilder.AppendLine($"Last updated at: {file.UpdatedAt.ToString("f", CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.ToString()))}");
                    listBuilder.AppendLine($"SHA-1 Hash: {file.SHA1Hash}");
                    listBuilder.AppendLine();
                }

                Console.WriteLine(listBuilder.ToString());
            }
        }

        /// <summary>
        /// Prints metadata about a Neocities website. By default this method
        /// will retrieve metadata about the website associated with the current
        /// account. If a website name is provided as an argument, this method
        /// will retrieve the metadata about that website
        /// </summary>
        /// <param name="websiteName">If provided, will retrieve the metadata of this site</param>
        public async void GetSiteData(string websiteName = "")
        {
            StringBuilder metadataBuilder = new StringBuilder();
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
                metadataBuilder.AppendLine($"Website Name: {websiteData.Attributes.SiteName}");
                metadataBuilder.AppendLine($"Number of website hits: {websiteData.Attributes.NumberOfHits}");
                metadataBuilder.AppendLine($"Site Creation Date: {websiteData.Attributes.CreatedAt.ToString("f", CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.ToString()))}");
                metadataBuilder.AppendLine($"Site Last Updated At: {websiteData.Attributes.LastUpdated.ToString("f", CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.ToString()))}");
                metadataBuilder.AppendLine($"Domain: {websiteData.Attributes.Domain}");
                metadataBuilder.Append("Website Tags: ");
                metadataBuilder.AppendJoin(", ", websiteData.Attributes.Tags);

                Console.WriteLine(metadataBuilder.ToString());
            }
        }
    }
}
