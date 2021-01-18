using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Infrastructure.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(country => country.Name)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(country => country.Flag)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
