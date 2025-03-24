using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RegionsAPI.Dto;
using System.Net.Http;

namespace RegionsAPI.Services
{
    public class RegionService : IRegionService
    {

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string CountriesCacheKey = "CountriesCacheKey";
        public RegionService(HttpClient httpClient, IMemoryCache cache) {
             _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<string?> GetCountriesDataAsync()
        {
            if (!_cache.TryGetValue(CountriesCacheKey, out string? countriesData))
            {
                countriesData = await _httpClient.GetStringAsync("https://restcountries.com/v3.1/all");

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                _cache.Set(CountriesCacheKey, countriesData, cacheEntryOptions);
            }

            return countriesData;
        }

        public async Task<List<CountriesDTO>> GetCountriesbyRegion(string regionID)
        {
            List<CountriesDTO> countriesDTO = new List<CountriesDTO>();
            var response = await GetCountriesDataAsync(); ;
            var countries = response != null ? JsonConvert.DeserializeObject<List<CountriesDTO>>(response)?.Where(c => c.Region == regionID) : null;
            countriesDTO = countries != null ? countries.ToList() : [];
            return countriesDTO;
        }

        public async Task<List<string?>> GetRegions()
        {
            List<string?> regions = new List<string?>();
            var response = await GetCountriesDataAsync();
            var countries = response != null ? JsonConvert.DeserializeObject<List<CountriesDTO>>(response) : null;
            regions = countries != null ? countries.Select(x=> x.Region).Distinct().ToList() : [];
            return regions;
        }

        public async Task<List<string>> GetCountriesbyFilter(string filter)
        {
            List<string> filterCountries = new List<string>();
            var response = await GetCountriesDataAsync(); ;
            var countries = response != null ? JsonConvert.DeserializeObject<List<CountriesDTO>>(response)?.Where(c => c.Name?.Official != null && c.Name?.Common != null ? c.Name.Official.ToLower().Contains(filter.ToLower()) || c.Name.Common.ToLower().Contains(filter) : false) : null;
            filterCountries = countries != null ? countries.Select(x => x.Name?.Common).Where(name => name != null).Distinct().Cast<string>().ToList() : [];
            return filterCountries;
        }

    }
}
