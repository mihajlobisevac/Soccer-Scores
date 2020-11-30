using Microsoft.EntityFrameworkCore;
using Scores.Domain.Models;

namespace Scores.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        DbSet<City> Cities { get; set; }
        DbSet<Club> Clubs { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Match> Matches { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Venue> Venues { get; set; }
    }
}
