using IBGE.Entities;
using IBGE.Interfaces;

namespace IBGE.Services
{
    public class LocationService : ILocationService
    {
        public Task Add(Location location)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ProcessExcelFileAsync(IFormFile excelFile)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Location>> Search(string search)
        {
            throw new NotImplementedException();
        }

        public Task Update(Location location)
        {
            throw new NotImplementedException();
        }
    }
}