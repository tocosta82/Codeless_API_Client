using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting API Client.");

            var myhost = AppStartup();

            await myhost.RunAsync();
        }

        private static IHost AppStartup()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((ctx, services) =>
                {
                    services.AddHostedService<HostedService>();
                })
                .Build();

            return host;
        }
    }
}
