using IBGE.Context;
using IBGE.Entities;
using IBGE.Interfaces;

namespace IBGE.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IbgeDbContext _context;
        private readonly IConfiguration _configuration;

        public LocationRepository(IbgeDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task Add(Location location)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertBatch(IEnumerable<Location> locations)
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