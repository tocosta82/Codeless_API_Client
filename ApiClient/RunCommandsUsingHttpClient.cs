using ApiClient.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class RunCommandsUsingHttpClient : RunCommandBase
    {
        public RunCommandsUsingHttpClient(string apiBaseURL)
            : base(apiBaseURL.TrimEnd('/') + "/api/ShoppingList", "HttpClient")
        {
        }

        public override async Task<ShoppingList> GetShoppingList()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiBaseURL);
                response.EnsureSuccessStatusCode();
                return await Deserialize(response);
            }
        }

        public override async Task PopulateDatabase()
        {
            using (var client = new HttpClient())
            {
                foreach(var item in ItemGenerator.Generate())
                {
                    var json = JsonConvert.SerializeObject(item);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    await client.PostAsync(ApiBaseURL, data);
                }
            }
        }

        public override async Task<ShoppingList> Update(ShoppingListItem item)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(item);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(ApiBaseURL, data);
                response.EnsureSuccessStatusCode();
                try
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    return await Deserialize(response);
                }
                catch // Could be ArgumentNullException or UnsupportedMediaTypeException
                {
                    Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
                    return null;
                }
            }
        }

        public override async Task<ShoppingList> Delete()
        {
            var list = await GetShoppingList();
            using (var client = new HttpClient())
            {
                foreach (var item in list)
                {
                    var response = await client.DeleteAsync(ApiBaseURL + "/" + item.Id);
                    response.EnsureSuccessStatusCode();
                }
            }

            return await GetShoppingList();
        }

        private async Task<ShoppingList> Deserialize(HttpResponseMessage response)
        {
            try
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                return serializer.Deserialize<ShoppingList>(jsonReader);
            }
            catch // Could be ArgumentNullException or UnsupportedMediaTypeException
            {
                Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
                return null;
            }
        }
    }
}
