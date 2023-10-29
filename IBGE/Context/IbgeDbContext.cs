using IBGE.Mappings;
using IBGE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IBGE.Context
{
    public class IbgeDbContext : IdentityDbContext<IdentityUser>
    {
        public IbgeDbContext(DbContextOptions<IbgeDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>(new LocationMap().Configure);
        }
    }
}