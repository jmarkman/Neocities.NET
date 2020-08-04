﻿using NeocitiesNET.AccountInteraction;
using System;
using System.Collections.Generic;

namespace NeocitiesNET
{
    public static class Extensions
    {
        /// <summary>
        /// Locates the <see cref="Account"/> object in the list with the specified name
        /// </summary>
        /// <param name="accounts">The list of <see cref="Account"/> objects</param>
        /// <param name="accountName">The name to search for</param>
        /// <returns>The index of the <see cref="Account"/> with the specified name</returns>
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

        /// <summary>
        /// Moves the index at the specified position to the desired position. By default, this method moves
        /// the account at the specified position to the top of the list (i.e., moveToIndex = 0)
        /// </summary>
        /// <param name="accounts">The list of <see cref="Account"/> objects</param>
        /// <param name="accountIndex">Get this using <see cref="FindIndexOfAccount(List{Account}, string))"/></param>
        /// <param name="moveToIndex">Defaults to 0 (top of the list)</param>
        public static void MoveAccountAtIndexTo(this List<Account> accounts, int accountIndex, int moveToIndex = 0)
        {
            Account acct = accounts[accountIndex];
            accounts.RemoveAt(accountIndex);
            accounts.Insert(moveToIndex, acct);
        }

        /// <summary>
        /// Converts a byte value to a kilobyte using the base 10 conversion rather
        /// than the base 2 conversion
        /// </summary>
        /// <param name="bytes">e.x., <see cref="NeocitiesApi.Models.File.Size"/></param>
        /// <returns>The byte value in base 10 kilobytes</returns>
        public static double ConvertFromBytesToBase10Kilobytes(this double bytes)
        {
            return Math.Round(bytes / 1000f, 1);
        }
    }
}
