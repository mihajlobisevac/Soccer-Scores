using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Infrastructure.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(player => player.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(player => player.LastName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
