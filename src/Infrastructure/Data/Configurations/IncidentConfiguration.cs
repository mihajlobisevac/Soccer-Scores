using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Infrastructure.Data.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.Property(incident => incident.PrimaryPlayer)
                .IsRequired(false);

            builder.Property(incident => incident.SecondaryPlayer)
                .IsRequired(false);
        }
    }
}
