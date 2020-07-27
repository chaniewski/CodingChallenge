using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerTests
    {
        [Fact]
        public async Task Countries_OnSecondInvoke_ShouldReturnCachedData()
        {
            // Arrange
            var restCountriesMock = Substitute.For<IRestCountriesApiService>();
            restCountriesMock.GetAllCountriesAsync().Returns(Task.FromResult(new List<Country>
            {
                new Country{Alpha2Code = "PL"}
            }));

            var builder = new WebHostBuilder().UseStartup<Startup>()
                .UseSetting("RestCountriesServiceBaseUrl", "https://restcountries.eu/rest/v2/")
                .ConfigureTestServices(services =>
                {
                    services.RemoveAll<IRestCountriesApiService>()
                        .TryAddSingleton(restCountriesMock);
                });

            var testServer = new TestServer(builder);
            var client = testServer.CreateClient();

            // Act
            var response1 = await client.GetAsync("/countries");
            var response2 = await client.GetAsync("/countries");

            // Assert
            response1.StatusCode.Should().Be(StatusCodes.Status200OK);
            response2.StatusCode.Should().Be(StatusCodes.Status200OK);

            var content1 = await response1.Content.ReadAsStringAsync();
            var content2 = await response2.Content.ReadAsStringAsync();

#pragma warning disable 4014 for .Received await is not required, so suppress warning “Consider applying the 'await' operator”
            restCountriesMock.Received(1).GetAllCountriesAsync();
#pragma warning restore 4014

            var countries1 = JsonConvert.DeserializeObject<List<Country>>(content1);
            countries1.Should().Contain(c => c.Alpha2Code == "PL");
            var countries2 = JsonConvert.DeserializeObject<List<Country>>(content2);
            countries2.Should().Contain(c => c.Alpha2Code == "PL");
        }

        [Fact]
        public async Task Countries_OnError_ShouldReturnHttp500()
        {
            // Arrange
            var restCountriesMock = Substitute.For<IRestCountriesApiService>();
            restCountriesMock.GetAllCountriesAsync().Throws(new Exception("Cannot retrieve countries"));

            var builder = new WebHostBuilder().UseStartup<Startup>()
                .UseSetting("RestCountriesServiceBaseUrl", "https://restcountries.eu/rest/v2/")
                .ConfigureTestServices(services =>
                {
                    services.RemoveAll<IRestCountriesApiService>()
                        .TryAddSingleton(restCountriesMock);
                });

            var testServer = new TestServer(builder);
            var client = testServer.CreateClient();

            // Act
            var response = await client.GetAsync("/countries");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
