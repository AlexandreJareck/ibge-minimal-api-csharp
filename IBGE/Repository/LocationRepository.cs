using IBGE.Context;
using IBGE.Entities;
using IBGE.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<IList<Location>> Search(string search)
        {
            try
            {
                return await _context.Locations
                    .AsNoTracking()
                    .Where(w => search.Equals(w.Id) || EF.Functions.Like(w.City, $"%{search}%"))
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("An internal error has occurred");
            }
        }

        public async Task Add(Location location)
        {
            Location? locationData = await GetById(location.Id);

            if (locationData is not null)
                throw new Exception("There is already a locality registred whit this code.");

            try
            {
                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An internal error has occurred");
            }
        }

        public async Task Update(Location location)
        {
            try
            {
                _context.Entry(location).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An internal error has occurred");
            }
        }

        public async Task Delete(string id)
        {
            Location? location = await GetById(id) ?? throw new Exception("Location not registred");

            try
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An internal error has occurred");
            }
        }

        public async Task<int> InsertBatch(IEnumerable<Location> localities)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await CreateTempTable(connection);

            try
            {
                using var transaction = connection.BeginTransaction();

                using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction);
                bulkCopy.DestinationTableName = "location_temp";
                bulkCopy.ColumnMappings.Add("Id", "id");
                bulkCopy.ColumnMappings.Add("State", "state");
                bulkCopy.ColumnMappings.Add("City", "city");

                using var dataTable = new DataTable();
                dataTable.Columns.Add("id", typeof(string));
                dataTable.Columns.Add("state", typeof(string));
                dataTable.Columns.Add("city", typeof(string));

                foreach (var locality in localities)
                {
                    dataTable.Rows.Add(locality.Id, locality.State, locality.City);
                }

                await bulkCopy.WriteToServerAsync(dataTable);

                transaction.Commit();

                return await InsertIntoLocality(connection);
            }
            catch (Exception ex)
            {
                throw new Exception("An internal error has occurred", ex);
            }
        }

        private static async Task CreateTempTable(SqlConnection connection)
        {
            using var cmd = new SqlCommand("CREATE TABLE location_temp (id nvarchar(MAX), state nvarchar(MAX), city nvarchar(MAX)", connection);
            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task<int> InsertIntoLocality(SqlConnection connection)
        {
            using var cmd = new SqlCommand("INSERT INTO ibge (id, state, city) SELECT id, state, city FROM ibge_temp WHERE NOT EXISTS (SELECT 1 FROM ibge WHERE ibge.id = ibge_temp.id)", connection);
            return await cmd.ExecuteNonQueryAsync();
        }

        private async Task<Location?> GetById(string id) => await _context.Locations.FirstOrDefaultAsync(x => id.Equals(x.Id));
    }
}