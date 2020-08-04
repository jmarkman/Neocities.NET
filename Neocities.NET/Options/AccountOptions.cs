using CommandLine;
using System.Collections.Generic;

namespace NeocitiesNET
{
    [Verb("account", HelpText = "Manage your various Neocities website credentials")]
    public class AccountOptions
    {
        [Option("addkey", Separator = ':', HelpText = "Add your website name and associated API key in the format [AccountName]:[API Key]")]
        public IEnumerable<string> AddApiKey { get; set; }

        [Option("addpass", Separator = ':', HelpText = "Add your username and password in the format [AccountName]:[Password]")]
        public IEnumerable<string> AddPassword { get; set; }

        [Option("updatekey", Separator = ':', HelpText = "Update the API key associated with the specified account in the format [AccountName]:[New API Key]")]
        public IEnumerable<string> UpdateApiKey { get; set; }

        [Option("updatepass", Separator = ':', HelpText = "Update the password associated with the specified account in the format [AccountName]:[New Password]")]
        public IEnumerable<string> UpdatePassword { get; set; }

        [Option("use", HelpText = "Specify the account to use")]
        public string UseAccount { get; set; }

        [Option("delete", HelpText = "Delete the information (API key, password) associated with the specified account")]
        public string DeleteAccountInfo { get; set; }
    }
}
