using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeocitiesNET.AccountInteraction
{
    /// <summary>
    /// Interacts with the JSON file that contains all of the
    /// accounts that the user attaches to this program
    /// </summary>
    public class AccountCommands
    {
        private readonly AccountManager _accountManager;

        public AccountCommands()
        {
            _accountManager = new AccountManager();
        }

        /// <summary>
        /// Adds a brand new account to the account list
        /// </summary>
        /// <param name="account">An <see cref="Account"/> in either username/pass or username/api key configuration)</param>
        /// <returns><see cref="true"/> if the addition was successful, <see cref="false"/> otherwise</returns>
        public bool AddAccount(AccountSecurityType securityType, List<string> account)
        {
            Account parsedAccount = ScaffoldAccount(securityType, account);

            return _accountManager.AddAccount(parsedAccount);
        }

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="securityType">The account security type that's being updated (either password or api key)</param>
        /// <param name="account">The account with updated details</param>
        /// <returns><see cref="true"/> if the update was successful, <see cref="false"/> otherwise</returns>
        public bool UpdateAccount(AccountSecurityType securityType, List<string> account)
        {
            Account parsedAccount = ScaffoldAccount(securityType, account);

            return _accountManager.UpdateAccount(securityType, parsedAccount);
        }

        /// <summary>
        /// Delete an account from the list of accounts
        /// </summary>
        /// <param name="accountName">The name of the account to delete</param>
        public bool DeleteAccount(string accountName)
        {
            Console.WriteLine($"Deleting '{accountName}' from the list of accounts");
            return _accountManager.DeleteAccount(accountName);
        }

        /// <summary>
        /// Get the account that will be used for API operations
        /// </summary>
        /// <returns>The <see cref="Account"/> object with either an API key or a password</returns>
        public Account GetActiveAccount()
        {
            return _accountManager.GetFirstAccount();
        }

        /// <summary>
        /// Sets the account to use for API operations
        /// </summary>
        /// <returns><see cref="true"/> if the operation was successful, <see cref="false"/> otherwise</returns>
        public bool SetActiveAccount(string accountName)
        {
            Console.WriteLine($"Setting '{accountName}' as the active account");
            return _accountManager.SetFirstAccount(accountName);
        }

        /// <summary>
        /// Create a <see cref="Account"/> object from the command line information. Used for
        /// adding or updating accounts.
        /// </summary>
        /// <param name="securityType">The type of API security this account will use to access the API</param>
        /// <param name="account">The list from the command line input</param>
        /// <returns>A <see cref="Account"/> object populated with the relevant information</returns>
        private Account ScaffoldAccount(AccountSecurityType securityType, List<string> account)
        {
            Account parsedAccount;

            if (securityType == AccountSecurityType.APIKey)
            {
                parsedAccount = new Account
                {
                    Username = account[0],
                    ApiKey = account[1]
                };
            }
            else
            {
                parsedAccount = new Account
                {
                    Username = account[0],
                    Password = account[1]
                };
            }

            return parsedAccount;
        }
    }
}
