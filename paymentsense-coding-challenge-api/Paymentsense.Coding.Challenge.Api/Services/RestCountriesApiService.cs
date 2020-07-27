using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class RestCountriesApiService : IRestCountriesApiService
    {
        private readonly HttpClient _client;

        public RestCountriesApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            var httpResponse = await _client.GetAsync("all");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve countries");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<Country>>(content);

            return countries;
        }
    }
}