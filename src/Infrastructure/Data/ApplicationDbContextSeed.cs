using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerScores.Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Cities.Any())
            {
                var cities = new List<City>
                {
                    new City { Name = "London", Country = new Country { Name = "England", Flag = "https://restcountries.eu/data/gbr.svg" } },
                    new City { Name = "Munich", Country = new Country { Name = "Germany", Flag = "https://restcountries.eu/data/deu.svg" } },
                    new City { Name = "Belgrade", Country = new Country { Name = "Serbia", Flag = "https://restcountries.eu/data/srb.svg" } },
                    new City { Name = "Barcelona", Country = new Country { Name = "Spain", Flag = "https://restcountries.eu/data/esp.svg" } },
                };

                cities.ForEach(x => context.Cities.Add(x));

                await context.SaveChangesAsync();
            }
        }
    }
}
