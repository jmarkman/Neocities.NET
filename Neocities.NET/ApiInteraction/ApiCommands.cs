using NeocitiesNET.AccountInteraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeocitiesNET.ApiInteraction
{
    public class ApiCommands
    {
        private readonly string _apiKey;
        private readonly Account _account;

        public ApiCommands(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ApiCommands(Account account)
        {
            _account = account;
        }
    }
}
