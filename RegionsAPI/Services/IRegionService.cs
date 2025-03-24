using RegionsAPI.Dto;

namespace RegionsAPI.Services
{
    public interface IRegionService
    {
        Task<string?> GetCountriesDataAsync();
        Task<List<CountriesDTO>> GetCountriesbyRegion(string regionID);

        Task<List<string?>> GetRegions();

        Task<List<string>> GetCountriesbyFilter(string filter);
    }
}
