using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeocitiesNET.AccountInteraction
{
    public enum AccountSecurityType
    {
        Password,
        APIKey
    }

    public class AccountManager
    {
        private readonly string _accountFile = "accounts.json";

        public AccountManager() { }

        /// <summary>
        /// Add an account to the account file
        /// </summary>
        /// <param name="account">Contains a username and either a password or an API key</param>
        public void AddAccount(Account account)
        {
            string json = JsonConvert.SerializeObject(account);

            File.AppendAllText(_accountFile, json);
        }

        /// <summary>
        /// Get the first account from the account file to use for API interaction
        /// </summary>
        /// <returns>The first account from the account file</returns>
        public Account GetFirstAccount()
        {
            var accounts = GetAllAccountsFromJson();

            return accounts.FirstOrDefault();
        }

        /// <summary>
        /// Get the specified account from the account file
        /// </summary>
        /// <param name="username">The name associated with the password/api key</param>
        /// <returns>An account object matching the provided username</returns>
        public Account GetAccount(string username)
        {
            var accounts = GetAllAccountsFromJson();

            return accounts.Where(a => a.Username == username).Select(a => a).FirstOrDefault();
        }

        /// <summary>
        /// Gets the specified account and sets it as the positionally first account
        /// in the account file for usage during
        /// </summary>
        /// <param name="username"></param>
        public void SetFirstAccount(string username)
        {
            var accounts = GetAllAccountsFromJson();
            var index = accounts.FindIndexOfAccount(username);

            accounts.MoveAccountAtIndexTo(index, moveToIndex: 0);

            var accountsJsonString = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(_accountFile, accountsJsonString);
        }

        public void UpdateAccount(AccountSecurityType securityType, Account updatedAccount)
        {
            var accounts = GetAllAccountsFromJson();
            var index = accounts.FindIndexOfAccount(updatedAccount.Username);

            switch (securityType)
            {
                case AccountSecurityType.Password:
                    accounts[index].Password = updatedAccount.Password;
                    break;
                case AccountSecurityType.APIKey:
                    accounts[index].ApiKey = updatedAccount.ApiKey;
                    break;
            }

            var accountsJsonString = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(_accountFile, accountsJsonString);
        }

        public void DeleteAccount(string username)
        {
            var accounts = GetAllAccountsFromJson();
            var index = accounts.FindIndexOfAccount(username);

            accounts.RemoveAt(index);

            var accountsJsonString = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(_accountFile, accountsJsonString);
        }

        public bool DoesAccountExist(Account acct)
        {
            return GetAllAccountsFromJson().Exists(a => a.Username == acct.Username);
        }

        private List<Account> GetAllAccountsFromJson()
        {
            string json = File.ReadAllText(_accountFile);
            return JsonConvert.DeserializeObject<List<Account>>(json);
        }
    }
}
