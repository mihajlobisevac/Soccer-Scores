using SoccerScores.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SoccerScores.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<Club> Clubs { get; set; }
        DbSet<Competition> Competitions { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Incident> Incidents { get; set; }
        DbSet<Match> Matches { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Season> Standings { get; set; }
    }
}
