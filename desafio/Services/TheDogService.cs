using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using desafio_api.Services.Models;
using Newtonsoft.Json;

namespace desafio_api.Services
{
    public class TheDogService : ITheDogService
    {
        private readonly HttpClient _httpClient;

        public TheDogService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.thedogapi.com/v1/"),
            };

            _httpClient.DefaultRequestHeaders.Add("x-api-key", "ab835eec-d5b4-46fa-8a50-1de2c80f7d7c");
        }

        public async Task<List<Breeds>> GetBreedByName(string name)
        {
            var response = await _httpClient.GetAsync($"breeds/search?q={name}");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Breeds>>(jsonString);
        }

        public async Task<List<Breeds>> GetBreeds()
        {
            var response = await _httpClient.GetAsync("breeds");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Breeds>>(jsonString);
        }
    }
}