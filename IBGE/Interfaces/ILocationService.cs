using IBGE.Entities;

namespace IBGE.Interfaces
{
    public interface ILocationService
    {
        Task Add(Location location);

        Task Update(Location location);

        Task Delete(string id);

        Task<IList<Location>> Search(string search);

        Task<int> ProcessExcelFileAsync(IFormFile excelFile);
    }
}