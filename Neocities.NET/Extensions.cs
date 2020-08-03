using NeocitiesNET.AccountInteraction;
using System.Collections.Generic;

namespace NeocitiesNET
{
    public static class Extensions
    {
        public static int FindIndexOfAccount(this List<Account> accounts, string accountName)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Username == accountName)
                {
                    return i;
                }
            }

            throw new AccountNotFoundException($"The account with name '{accountName}' could not be located in the sequence!");
        }

        public static void MoveAccountAtIndexTo(this List<Account> accounts, int accountIndex, int moveToIndex = 0)
        {
            Account acct = accounts[accountIndex];
            accounts.RemoveAt(accountIndex);
            accounts.Insert(moveToIndex, acct);
        }
    }
}
