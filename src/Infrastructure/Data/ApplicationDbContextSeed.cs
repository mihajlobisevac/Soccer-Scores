using SoccerScores.Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SoccerScores.Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Cities.Any())
            {
                var countries = new List<Country>
                {
                    new Country { Name = "England", Flag = "https://restcountries.eu/data/gbr.svg" },
                    new Country { Name = "Germany", Flag = "https://restcountries.eu/data/deu.svg" },
                    new Country { Name = "Serbia", Flag = "https://restcountries.eu/data/srb.svg" },
                    new Country { Name = "Spain", Flag = "https://restcountries.eu/data/esp.svg" },
                };

                context.Countries.AddRange(countries);

                var cities = new List<City>
                {
                    new City { Name = "London", Country = countries[0] },
                    new City { Name = "Manchester", Country = countries[0] },
                    new City { Name = "Munich", Country = countries[1] },
                    new City { Name = "Belgrade", Country = countries[2] },
                    new City { Name = "Barcelona", Country = countries[3] },
                    new City { Name = "Madrid", Country = countries[3] },
                };

                context.Cities.AddRange(cities);

                var competitions = new List<Competition>
                {
                    new Competition { Name = "Premier League", Type = CompetitionType.League, Country = countries[0] },
                    new Competition { Name = "Bundesliga", Type = CompetitionType.League, Country = countries[1] },
                    new Competition { Name = "Super Liga", Type = CompetitionType.League, Country = countries[2] },
                    new Competition { Name = "La Liga", Type = CompetitionType.League, Country = countries[3] },
                };

                context.Competitions.AddRange(competitions);

                var seasons = new List<Season>
                {
                    new Season { Start = new DateTime(2020, 9, 12), End = new DateTime(2021, 5, 14), Competition = competitions[0] },
                    new Season { Start = new DateTime(2010, 9, 12), End = new DateTime(2011, 5, 14), Competition = competitions[1] },
                    new Season { Start = new DateTime(2014, 9, 12), End = new DateTime(2015, 5, 14), Competition = competitions[2] },
                    new Season { Start = new DateTime(2007, 9, 12), End = new DateTime(2008, 5, 14), Competition = competitions[3] },
                };

                context.Seasons.AddRange(seasons);

                await context.SaveChangesAsync();
            }
        }
    }
}
