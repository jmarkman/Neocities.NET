using CommandLine;
using NeocitiesNET.AccountInteraction;
using NeocitiesNET.ApiInteraction;
using NeocitiesNET.Options;
using System.Linq;
using System.Threading.Tasks;

namespace NeocitiesNET
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<AccountOptions, ApiGet, ApiModify>(args)
                .MapResult(
                    (AccountOptions acctOpts) => RunAccountCommand(acctOpts),
                    (ApiGet getOpts) => ApiGetCommands(getOpts),
                    (ApiModify modifyOpts) => ApiModifyCommands(modifyOpts),
                    _ => Task.FromResult(1)
                );
        }

        private static async Task<int> ApiModifyCommands(ApiModify modifyOpts)
        {
            AccountCommands acctCommands = new AccountCommands();
            var account = acctCommands.GetActiveAccount();
            ApiCommands apiCommands = new ApiCommands(account);

            if (modifyOpts.DeleteFiles.Count() > 0)
            {
                if (await apiCommands.Delete(modifyOpts.DeleteFiles))
                {
                    return 0;
                }

                return 1;
            }
            else if (!string.IsNullOrWhiteSpace(modifyOpts.UploadFile))
            {
                if (await apiCommands.Upload(modifyOpts.UploadFile))
                {
                    return 0;
                }

                return 1;
            }

            return 1;
        }

        private static async Task<int> ApiGetCommands(ApiGet apiOption)
        {
            AccountCommands acctCommands = new AccountCommands();
            var account = acctCommands.GetActiveAccount();
            ApiCommands apiCommands = new ApiCommands(account);
            
            if (apiOption.ListAllFiles)
            {
                await apiCommands.ListAllFiles();
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(apiOption.ListFilesFromDirectory))
            {
                await apiCommands.ListAllFiles(apiOption.ListFilesFromDirectory);
                return 0;
            }
            else if (apiOption.GetPersonalMetadata)
            {
                await apiCommands.GetSiteData();
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(apiOption.GetSiteMetadata))
            {
                await apiCommands.GetSiteData(apiOption.GetSiteMetadata);
                return 0;
            }
            else if (apiOption.GetApiKey)
            {
                await apiCommands.GetSiteKey();
                return 0;
            }

            return 1;
        }

        private static async Task<int> RunAccountCommand(AccountOptions acctOption)
        {
            AccountCommands acctCommands = new AccountCommands();

            if (acctOption.AddApiKey.Count() > 1)
            {
                if (acctCommands.AddAccount(AccountSecurityType.APIKey, acctOption.AddApiKey.ToList()))
                {
                    return 0;
                }

                return 1;
            }
            else if (acctOption.AddPassword.Count() > 1)
            {
                if (acctCommands.AddAccount(AccountSecurityType.Password, acctOption.AddApiKey.ToList()))
                {
                    return 0;
                }

                return 1;
            }
            else if (acctOption.UpdateApiKey.Count() > 1)
            {
                if (acctCommands.UpdateAccount(AccountSecurityType.APIKey, acctOption.AddApiKey.ToList()))
                {
                    return 0;
                }

                return 1;
            }
            else if (acctOption.UpdatePassword.Count() > 1)
            {
                if (acctCommands.UpdateAccount(AccountSecurityType.Password, acctOption.AddApiKey.ToList()))
                {
                    return 0;
                }

                return 1;
            }
            else if (!string.IsNullOrWhiteSpace(acctOption.DeleteAccountInfo))
            {
                if (acctCommands.DeleteAccount(acctOption.DeleteAccountInfo))
                {
                    return 0;
                }

                return 1;
            }
            else if (!string.IsNullOrWhiteSpace(acctOption.UseAccount))
            {
                if (acctCommands.SetActiveAccount(acctOption.UseAccount))
                {
                    return 0;
                }

                return 1;
            }

            return 1;
        }
    }
}
