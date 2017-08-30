using Memoriser.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Memoriser.IntegrationTests
{
    public class StartupTests
    {
        [Fact]
        public async Task Can_Startup()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder =>
                    builder.AddJsonFile("appsettings.json")));
            var httpClient = server.CreateClient();

            var response = await server.CreateRequest("/api/hello").SendAsync("GET");
            response.EnsureSuccessStatusCode();
            var hello = await response.Content.ReadAsStringAsync();
            hello.Should().Be("Hello.");
        }
    }
}
