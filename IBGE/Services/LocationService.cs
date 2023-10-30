using IBGE.Entities;
using IBGE.Interfaces;

namespace IBGE.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<IList<Location>> Search(string search)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Location location)
        {
            await _locationRepository.Add(location);
        }

        public async Task Update(Location location)
        {
            await _locationRepository.Update(location);
        }

        public async Task Delete(string id)
        {
            await _locationRepository.Delete(id);
        }

        public Task<int> ProcessExcelFileAsync(IFormFile excelFile)
        {
            throw new NotImplementedException();
        }
    }
}