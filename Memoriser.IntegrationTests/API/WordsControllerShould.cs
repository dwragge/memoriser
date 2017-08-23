﻿using FluentAssertions;
using Memoriser_App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Memoriser.IntegrationTests.API
{
    public class WordsControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _httpClient;

        public WordsControllerShould()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _httpClient = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnListOfWords()
        {
            var response = await _httpClient.GetAsync("/api/words");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<IEnumerable<string>>(responseString)
                .ShouldBeEquivalentTo(new[]
                {
                    "hello",
                    "bonjour",
                    "le monde"
                });
        }
    }
}
