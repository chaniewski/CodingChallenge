using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IRestCountriesApiService _restCountriesApi;

        public CountriesController(IRestCountriesApiService restCountriesApi)
        {
            _restCountriesApi = restCountriesApi;
        }

        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<List<Country>>> Get()
        {
            var countries = await _restCountriesApi.GetAllCountriesAsync();
            return Ok(countries);
        }
    }
}
