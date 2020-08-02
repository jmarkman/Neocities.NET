using System;
using System.Collections.Generic;
using System.Text;

namespace Neocities.NET
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
    }
}
