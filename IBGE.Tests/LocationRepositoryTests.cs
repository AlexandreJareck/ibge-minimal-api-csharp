using IBGE.Context;
using IBGE.Entities;
using IBGE.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IBGE.Tests
{
    public class Tests
    {
        private IbgeDbContext _dbContext;
        private LocationRepository _LocationRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<IbgeDbContext>()
           .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            _dbContext = new IbgeDbContext(options);
            _dbContext.Database.EnsureCreated();

            // Configuração em memória
            var _configuration = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json")
            .Build();

            _LocationRepository = new LocationRepository(_dbContext, _configuration);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task Should_Inserts_Ibge_Entity()
        {
            var location = new Location { Id = "4319307", State = "SP", City = "São Paulo" };

            // Act
            await _LocationRepository.Add(location);

            // Assert
            var insertedLocation = await _dbContext.Locations.FirstOrDefaultAsync(x => "4319307".Equals(x.Id));
            Assert.That(insertedLocation, Is.EqualTo(location), "A entidade deveria ter sindo inserida");
        }
    }
}