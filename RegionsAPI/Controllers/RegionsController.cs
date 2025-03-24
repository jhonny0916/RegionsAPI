using Microsoft.AspNetCore.Mvc;
using RegionsAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace RegionsAPI.Controllers
{
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionService _regionService;
        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        [Route("api/Regions/findCountries/{regionID}")]
        public async virtual Task<IActionResult> GetCountries([FromRoute][Required] string regionID)
        {
            var countries = await _regionService.GetCountriesbyRegion(regionID);
            return Ok(countries);
        }

        [HttpGet]
        [Route("api/Regions/getRegions")]
        public async virtual Task<IActionResult> GetRegions()
        {
            var countries = await _regionService.GetRegions();
            return Ok(countries);
        }

        [HttpGet]
        [Route("api/Regions/getCountriesbyFilter/{filter}")]
        public async virtual Task<IActionResult> GetCountriesbyFilter([FromRoute][Required] string filter)
        {
            var countries = await _regionService.GetCountriesbyFilter(filter);
            return Ok(countries);
        }
    }
}
