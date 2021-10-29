using ApiClient.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiClient
{
    public class RunCommandsUsingRestsharp : RunCommandBase
    {
        private RestClient client;

        public RunCommandsUsingRestsharp(string apiBaseURL)
            : base(apiBaseURL.TrimEnd('/') + "/api/ShoppingList", "Restsharp")
        {
            client = new RestClient(ApiBaseURL);
        }

        public override async Task<ShoppingList> GetShoppingList()
        {
            //var client = new RestClient(ApiBaseURL);
            var request = new RestRequest(Method.GET);
            var result = await client.ExecuteAsync<ShoppingList>(request);
            if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            return result.Data;
        }

        public override async Task PopulateDatabase()
        {
            //var client = new RestClient(ApiBaseURL);
            foreach (var item in ItemGenerator.Generate())
            {
                var request = new RestRequest(Method.POST);
                request.AddParameter(
                    "application/json",
                    JsonConvert.SerializeObject(item),
                    ParameterType.RequestBody
                    );

                var result = await client.ExecuteAsync(request);
                if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            }
        }

        public override async Task<ShoppingList> Update(ShoppingListItem item)
        {
            //var client = new RestClient(ApiBaseURL);
            var request = new RestRequest(Method.PUT);
            request.AddParameter("application/json", JsonConvert.SerializeObject(item), ParameterType.RequestBody);
            var result = await client.ExecuteAsync<ShoppingList>(request);
            if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            return result.Data;
        }

        public override async Task<ShoppingList> Delete()
        {
            //var client = new RestClient(ApiBaseURL);
            var list = await GetShoppingList();
            foreach(var item in list)
            {
                var request = new RestRequest("/" + item.Id, Method.DELETE);
                var result = await client.ExecuteAsync(request);
                if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            }

            return await GetShoppingList();
        }
    }
}
