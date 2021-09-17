using ApiClient.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class RunCommandsUsingRestsharp : RunCommandBase
    {
        public RunCommandsUsingRestsharp(string apiBaseURL)
            : base(apiBaseURL.TrimEnd('/') + "/api/ShoppingList", "Restsharp")
        {
        }

        public override async Task<ShoppingList> GetShoppingList()
        {
            throw new NotImplementedException();
        }

        public override async Task PopulateDatabase()
        {
            throw new NotImplementedException();
        }

        public override async Task<ShoppingList> Update(ShoppingListItem item)
        {
            throw new NotImplementedException();
        }

        public override async Task<ShoppingList> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
