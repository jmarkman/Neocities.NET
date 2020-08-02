using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace Neocities.NET
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

        public void AddAccount(Account account)
        {
            string json = JsonConvert.SerializeObject(account);

            File.AppendAllText("accounts.json", json);
        }

        public Account GetAccount(string username)
        {
            var accounts = GetAllAccountsFromJson();

            return accounts.Where(a => a.Username == username).Select(a => a).FirstOrDefault();
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
