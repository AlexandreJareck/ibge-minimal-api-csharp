using IBGE.Entities;

namespace IBGE.Interfaces
{
    public interface ILocationRepository
    {
        Task Add(Location location);

        Task Update(Location location);

        Task Delete(string id);

        Task<int> InsertBatch(IEnumerable<Location> locations);

        Task<IList<Location>> Search(string search);
    }
}