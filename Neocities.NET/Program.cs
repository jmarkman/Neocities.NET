using CommandLine;
using NeocitiesNET.AccountInteraction;
using NeocitiesNET.ApiInteraction;
using NeocitiesNET.Options;
using System;
using System.Linq;

namespace NeocitiesNET
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<AccountOptions, ApiGet, ApiModify>(args)
                .MapResult(
                    (AccountOptions acctOpts) => RunAccountCommand(acctOpts),
                    (ApiGet getOpts) => ApiGetCommands(getOpts),
                    (ApiModify modifyOpts) => ApiModifyCommands(modifyOpts),
                    _ => 1
                );
        }

        private static int ApiModifyCommands(ApiModify modifyOpts)
        {
            AccountCommands acctCommands = new AccountCommands();
            var account = acctCommands.GetActiveAccount();
            ApiCommands apiCommands = new ApiCommands(account);

            if (modifyOpts.DeleteFiles.Count() > 0)
            {
                // Delete files
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(modifyOpts.UploadFile))
            {
                // Upload file
                return 0;
            }

            return 1;
        }

        private static int ApiGetCommands(ApiGet apiOption)
        {
            AccountCommands acctCommands = new AccountCommands();
            var account = acctCommands.GetActiveAccount();
            ApiCommands apiCommands = new ApiCommands(account);
            
            if (apiOption.ListAllFiles)
            {
                apiCommands.ListAllFiles();
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

            return 1;
        }

        private static int RunAccountCommand(AccountOptions acctOption)
        {
            AccountCommands acctCommands = new AccountCommands();

            if (acctOption.AddApiKey.Count() > 1)
            {
                var success = acctCommands.AddAccount(AccountSecurityType.APIKey, acctOption.AddApiKey.ToList());

                if (success)
                {
                    return 0;
                }

                return 1;
            }
            else if (acctOption.AddPassword.Count() > 1)
            {
                var success = acctCommands.AddAccount(AccountSecurityType.Password, acctOption.AddApiKey.ToList());

                if (success)
                {
                    return 0;
                }

                return 1;
            }
            else if (acctOption.UpdateApiKey.Count() > 1)
            {
                var success = acctCommands.UpdateAccount(AccountSecurityType.APIKey, acctOption.AddApiKey.ToList());

                if (success)
                {
                    return 0;
                }

                return 1;
            }
            else if (acctOption.UpdatePassword.Count() > 1)
            {
                var success = acctCommands.UpdateAccount(AccountSecurityType.Password, acctOption.AddApiKey.ToList());

                if (success)
                {
                    return 0;
                }

                return 1;
            }
            else if (!string.IsNullOrWhiteSpace(acctOption.DeleteAccountInfo))
            {
                acctCommands.DeleteAccount(acctOption.DeleteAccountInfo);
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
