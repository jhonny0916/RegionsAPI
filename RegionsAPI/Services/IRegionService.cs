using RegionsAPI.Dto;

namespace RegionsAPI.Services
{
    public interface IRegionService
    {
        Task<List<CountriesDTO>> GetCountriesAsync(string regionID);

        Task<List<string?>> GetRegions();
    }
}
