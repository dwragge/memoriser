using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Memoriser.App;
using Microsoft.Extensions.Configuration;

namespace Memoriser.IntegrationTests.API
{
    public class WordsControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _httpClient;

        public WordsControllerShould()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder => 
                    builder.AddJsonFile("appsettings.json")));
            _httpClient = _server.CreateClient();
        }
        
    }
}
