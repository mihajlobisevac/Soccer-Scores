using Microsoft.EntityFrameworkCore;
using Scores.Domain.Models;
using System;

namespace Scores.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);

        public DbSet<City> Cities { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Standings> Standings { get; set; }
        public DbSet<ClubStandings> ClubStandings { get; set; }
    }
}
