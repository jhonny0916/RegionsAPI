namespace RegionsAPI.Services
{
    public class CacheInitializerService : IHostedService
    {
        private readonly IRegionService _regionService;

        public CacheInitializerService(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _regionService.GetCountriesAsync(string.Empty);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
