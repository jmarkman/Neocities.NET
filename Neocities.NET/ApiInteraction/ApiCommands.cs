using NeocitiesApi;
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
        /// List all of the files present on the current website
        /// </summary>
        public async void ListAllFiles()
        {
            StringBuilder listBuilder = new StringBuilder();
            var allFiles = await _apiClient.GetWebsiteFileListAsync();

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
        /// List files in the provided directory on the current website
        /// </summary>
        /// <param name="directory"></param>
        public async void ListFilesIn(string directory)
        {
            throw new NotImplementedException();
        }
    }
}
