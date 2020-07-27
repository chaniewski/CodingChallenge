using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerTests
    {
        [Fact]
        public async void Get_OnInvoke_ReturnsExpectedMessage()
        {
            var controller = new CountriesController(
                new RestCountriesApiService(
                    new HttpClient{BaseAddress = new Uri("https://restcountries.eu/rest/v2/") }));

            var result = (await controller.Get()).Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeAssignableTo<List<Country>>()
                .Which.Should().Contain(c => c.Alpha2Code == "PL");
        }
    }
}
