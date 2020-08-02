using System;
using System.Collections.Generic;
using System.Text;

namespace Neocities.NET
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
        public string Password { get; set; }


        /// <summary>
        /// The API key associated with this account, if present
        /// </summary>
        public string ApiKey { get; set; }
    }
}
