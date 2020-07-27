using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Models;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public CountriesControllerIntegrationTests()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>()
                .UseSetting("RestCountriesServiceBaseUrl", "https://restcountries.eu/rest/v2/");
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Countries_OnInvoke_ReturnsCountries()
        {
            // Act
            var response = await _client.GetAsync("/countries");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var countries = JsonConvert.DeserializeObject<List<Country>>(content);
            countries.Should().Contain(c => c.Alpha2Code == "PL");
        }
    }
}
