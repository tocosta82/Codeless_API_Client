using ApiClient.Model;
using Newtonsoft.Json;
using Refit;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class RunCommandsUsingRefit : RunCommandBase
    {
        private IRefitClient _refitClient;

        public RunCommandsUsingRefit(string apiBaseURL)
            : base(apiBaseURL.TrimEnd('/') + "/api/ShoppingList", "Refit")
        {
            _refitClient = RestService.For<IRefitClient>(ApiBaseURL);
        }

        public override async Task<ShoppingList> GetShoppingList()
        {
            return await _refitClient.GetShoppingList();
        }

        public override async Task PopulateDatabase()
        {
            foreach(var item in ItemGenerator.Generate())
            {
                await _refitClient.PostItem(item);
            }
        }

        public override async Task<ShoppingList> Update(ShoppingListItem item)
        {
            return await _refitClient.Update(item);
        }

        public override async Task<ShoppingList> Delete()
        {
            var list = await GetShoppingList();

            foreach(var item in list)
            {
                await _refitClient.Delete(item.Id.Value);
            }

            return await GetShoppingList();
        }
    }

    public interface IRefitClient
    {
        [Get("/")]
        public Task<ShoppingList> GetShoppingList();
        [Post("/")]
        public Task PostItem(ShoppingListItem item);
        [Put("/")]
        public Task<ShoppingList> Update(ShoppingListItem item);
        [Delete("/{id}")]
        public Task<ShoppingList> Delete([AliasAs("id")] Guid itemId);

    }
}
