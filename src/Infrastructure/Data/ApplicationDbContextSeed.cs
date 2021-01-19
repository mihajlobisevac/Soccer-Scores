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
                    new Season { Start = new DateTime(2019, 9, 12), End = new DateTime(2020, 5, 14), Competition = competitions[0] },
                    new Season { Start = new DateTime(2018, 9, 12), End = new DateTime(2019, 5, 14), Competition = competitions[0] },
                    new Season { Start = new DateTime(2010, 9, 12), End = new DateTime(2011, 5, 14), Competition = competitions[1] },
                    new Season { Start = new DateTime(2014, 9, 12), End = new DateTime(2015, 5, 14), Competition = competitions[2] },
                    new Season { Start = new DateTime(2007, 9, 12), End = new DateTime(2008, 5, 14), Competition = competitions[3] },
                };

                context.Seasons.AddRange(seasons);

                var clubs = new List<Club>
                {
                    new Club { Name = "F.C. Barcelona", Venue = "Camp Nou", YearFounded = 1899, Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/2017.png", Participations = new List<Season> { seasons[6] } },
                    new Club { Name = "Real Madrid C.F.", Venue = "Santiago Bernabéu", YearFounded = 1899, Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/2016.png", Participations = new List<Season> { seasons[6] } },
                    new Club { Name = "Liverpool", Venue = "Anfield", YearFounded = 1899, Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/663.png", Participations = new List<Season> { seasons[0], seasons[1], seasons[2] } },
                    new Club { Name = "Manchester United", Venue = "Old Trafford", YearFounded = 1899, Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/662.png", Participations = new List<Season> { seasons[0], seasons[1], seasons[2] } },
                };

                context.Clubs.AddRange(clubs);

                var players = new List<Player>
                {
                    new Player
                    {
                        FirstName = "Lionel",
                        LastName = "Messi",
                        DateOfBirth = new DateTime(1987, 6, 24),
                        Position = Position.Attacker,
                        Foot = Foot.Left,
                        Height = 170,
                        Weight = 72,
                        Nationality = countries[0],
                        PlaceOfBirth = cities[0],
                    },
                    new Player
                    {
                        FirstName = "Ansu",
                        LastName = "Fati",
                        DateOfBirth = new DateTime(2001, 6, 24),
                        Position = Position.Attacker,
                        Foot = Foot.Right,
                        Height = 178,
                        Weight = 66,
                        Nationality = countries[3],
                        PlaceOfBirth = cities[4],
                    },
                    new Player
                    {
                        FirstName = "Karim",
                        LastName = "Benzema",
                        DateOfBirth = new DateTime(1987, 6, 24),
                        Position = Position.Attacker,
                        Foot = Foot.Right,
                        Height = 185,
                        Weight = 81,
                        Nationality = countries[1],
                        PlaceOfBirth = cities[2],
                    },
                };

                context.Players.AddRange(players);

                var clubPlayers = new List<ClubPlayer>
                {
                    new ClubPlayer
                    {
                        ShirtNumber = 10,
                        Club = clubs[0],
                        Player = players[0],
                    },
                    new ClubPlayer
                    {
                        ShirtNumber = 17,
                        Club = clubs[0],
                        Player = players[1],
                    },
                    new ClubPlayer
                    {
                        ShirtNumber = 9,
                        Club = clubs[1],
                        Player = players[2],
                    },
                };

                context.ClubPlayers.AddRange(clubPlayers);

                await context.SaveChangesAsync();
            }
        }
    }
}
