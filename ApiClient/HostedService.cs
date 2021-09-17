using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClient
{
    public class HostedService : IHostedService
    {
        private string _apiUrl;
        private char[] validChoices = new[] { '1', '2', '3' };
        private char[] testValidChoices = new[] { '4', '5', '6' };

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            char userChoice;
            Console.Write("api url: ");
            _apiUrl = Console.ReadLine();
            Console.Clear();
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("||         Run the Client using:            ||");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. HttpClient");
                Console.WriteLine("2. RestSharp");
                Console.WriteLine("3. Refit");
                Console.WriteLine("4. Test Time HttpClient");
                Console.WriteLine("5. Test Time RestSharp");
                Console.WriteLine("6. Test Time Refit");
                Console.WriteLine("7. Test All");
                Console.WriteLine("==============================================");
                Console.WriteLine("[Everything else will exit the application]");

                userChoice = Console.ReadKey().KeyChar;
                Console.Clear();

                if (!IsValidChoice(userChoice))
                {
                    Console.WriteLine("Thanks for using the API Client");
                    continue;
                }

                var commandRunner = HandleOptions(userChoice);
                if(validChoices.Contains(userChoice))
                    await commandRunner.Run();
                if (testValidChoices.Contains(userChoice))
                    await commandRunner.TestTimes();
                if (userChoice == '7')
                    await RunAllTests();
            } while (IsValidChoice(userChoice));
        }

        private async Task RunAllTests()
        {
            await HandleOptions('4').TestTimes();
            Console.WriteLine("==============================================");
            await HandleOptions('5').TestTimes();
            Console.WriteLine("==============================================");
            await HandleOptions('6').TestTimes();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private RunCommandBase HandleOptions(char userChoice)
        {
            switch (userChoice)
            {
                case '1':
                case '4':
                    return new RunCommandsUsingHttpClient(_apiUrl);
                case '2':
                case '5':
                    return new RunCommandsUsingRestsharp(_apiUrl);
                case '3':
                case '6':
                    return new RunCommandsUsingRefit(_apiUrl);

                default:
                    return null; //will never happen
            }
        }

        private bool IsValidChoice(char choice)
        {
            return choice == '7' || validChoices.Concat(testValidChoices).Contains(choice);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
