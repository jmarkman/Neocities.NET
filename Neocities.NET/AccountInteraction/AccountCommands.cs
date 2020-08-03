using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeocitiesNET.AccountInteraction
{
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

            if (_accountManager.DoesAccountExist(parsedAccount))
            {
                return false;
            }
            else
            {
                _accountManager.AddAccount(parsedAccount);
                return true;
            } 
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

            if (_accountManager.DoesAccountExist(parsedAccount))
            {
                _accountManager.UpdateAccount(securityType, parsedAccount);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteAccount(string accountName)
        {
            _accountManager.DeleteAccount(accountName);
        }

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
