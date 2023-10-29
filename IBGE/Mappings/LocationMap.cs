using IBGE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IBGE.Mappings
{
    public class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("location");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("CHAR(7)");

            builder.Property(prop => prop.State)
                .IsRequired()
                .HasColumnName("state")
                .HasColumnType("CHAR(2)");

            builder.Property(prop => prop.City)
                .IsRequired()
                .HasColumnName("city")
                .HasColumnType("VARCHAR(80)");
        }
    }
}