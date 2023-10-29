using IBGE.Context;
using IBGE.Entities;
using IBGE.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IBGE.Tests
{
    [TestFixture]
    [Category("Location Fixture Tests")]
    public class LocationRepositoryTests
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
        [Category("Add Location valid")]
        public async Task Should_Inserts_Location_Entity()
        {
            // Arange
            var location = new Location { Id = "4319307", State = "SP", City = "São Paulo" };

            // Act
            await _LocationRepository.Add(location);

            // Assert
            var insertedLocation = await _dbContext.Locations.FirstOrDefaultAsync(x => "4319307".Equals(x.Id));
            Assert.That(insertedLocation, Is.EqualTo(location), "A entidade deveria ter sindo inserida");
        }

        [Test]
        [Category("Remove location by id")]
        public async Task Should_Remove_Location_EntityById()
        {
            // Arrange
            var location = new Location { Id = "4319307", State = "SP", City = "São Paulo" };
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();

            // Act
            await _LocationRepository.Delete(location.Id);

            // Assert
            var deletedLocation = await _dbContext.Locations.FirstOrDefaultAsync(x => "4319307".Equals(x.Id));
            Assert.That(deletedLocation, Is.Null, "A entidade deveria ter sido excluída");
        }
    }
}