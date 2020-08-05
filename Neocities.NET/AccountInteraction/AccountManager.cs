using Newtonsoft.Json;
using System;
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

    /// <summary>
    /// Manages the Neocities website accounts that the user owns, allowing for
    /// the user to switch between updating/querying the Neocities websites
    /// owned by the user
    /// </summary>
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
            var accounts = GetAllAccountsFromJson();

            accounts.Add(account);

            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);

            File.WriteAllText(_accountFile, json);
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
        /// in the account file when setting the primary account
        /// </summary>
        /// <param name="username">The username of the account that should be positionally first</param>
        public void SetFirstAccount(string username)
        {
            var accounts = GetAllAccountsFromJson();
            var index = accounts.FindIndexOfAccount(username);

            if (index == 0)
            {
                Console.WriteLine($"Account '{username}' is already first in the list!");
                return;
            }

            accounts.MoveAccountAtIndexTo(index, moveToIndex: 0);

            var accountsJsonString = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(_accountFile, accountsJsonString);
        }

        /// <summary>
        /// Updates the specified account in the account list with the details provided in the
        /// <see cref="Account"/> object
        /// </summary>
        /// <param name="securityType">The value to update: password or api key</param>
        /// <param name="updatedAccount">The account containing the username to update and the updated value (password/api key)</param>
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

        /// <summary>
        /// Removes an account from the account file
        /// </summary>
        /// <param name="username">The username of the account to remove</param>
        public void DeleteAccount(string username)
        {
            var accounts = GetAllAccountsFromJson();
            var index = accounts.FindIndexOfAccount(username);

            accounts.RemoveAt(index);

            var accountsJsonString = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(_accountFile, accountsJsonString);
        }

        /// <summary>
        /// Determine if the specified <see cref="Account"/> object exists in the
        /// account file 
        /// </summary>
        /// <param name="username">The name of the account to check</param>
        /// <returns><see cref="true"/> if the account exists in the file, <see cref="false"/> otherwise</returns>
        public bool DoesAccountExist(string username)
        {
            return GetAllAccountsFromJson().Exists(a => a.Username == username);
        }

        /// <summary>
        /// Reads the account file into a list
        /// </summary>
        /// <returns>A list of <see cref="Account"/> objects</returns>
        private List<Account> GetAllAccountsFromJson()
        {
            string json = File.ReadAllText(_accountFile);
            return JsonConvert.DeserializeObject<List<Account>>(json);
        }
    }
}
