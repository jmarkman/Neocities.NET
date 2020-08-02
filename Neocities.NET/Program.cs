﻿using CommandLine;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Neocities.NET
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<AccountOptions, ApiOptions>(args)
                .MapResult(
                    (AccountOptions acctOpts) => RunAccountCommand(acctOpts),
                    (ApiOptions apiOpts) => RunApiCommand(apiOpts),
                    _ => Task.FromResult(1)
                );
        }

        private static async Task<int> RunApiCommand(ApiOptions apiOption)
        {
            if (apiOption.ListAllFiles)
            {
                // List all files on website
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(apiOption.ListFilesFromDirectory))
            {
                // List files from specified directory
                return 0;
            }
            else if (apiOption.GetPersonalMetadata)
            {
                // Print personal site metadata
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(apiOption.GetSiteMetadata))
            {
                // Print metadata for specific site
                return 0;
            }
            else if (apiOption.GetApiKey)
            {
                // Print api key
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(apiOption.UploadFile))
            {
                // Upload file
                return 0;
            }
            else if (apiOption.DeleteFiles.Count() >= 1)
            {
                // Delete file or files from website
                return 0;
            }

            return 1;
        }

        private static async Task<int> RunAccountCommand(AccountOptions acctOption)
        {
            if (acctOption.AddApiKey.Count() > 1)
            {
                // Add an account and associated api key
                return 0;
            }
            else if (acctOption.AddAccount.Count() > 1)
            {
                // Add account and password
                return 0;
            }
            else if (acctOption.UpdateApiKey.Count() > 1)
            {
                // Update api key for account
                return 0;
            }
            else if (acctOption.UpdatePassword.Count() > 1)
            {
                // Update password for account
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(acctOption.DeleteAccountInfo))
            {
                // Delete account
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(acctOption.UseAccount))
            {
                // Switch to certain account for api interactions
                return 0;
            }

            return 1;
        }
    }
}
