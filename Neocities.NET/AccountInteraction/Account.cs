using System;
using System.Collections.Generic;
using System.Text;

namespace NeocitiesNET.AccountInteraction
{
    public class Account
    {
        public Account()
        {

        }

        /// <summary>
        /// The username of this account
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password associated with this account
        /// </summary>
        public string Password { get; set; } = string.Empty;


        /// <summary>
        /// The API key associated with this account, if present
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
