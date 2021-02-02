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
                    new Country { Name = "Europe", Flag = "https://restcountries.eu/data/esp.svg" },
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
                    new City { Name = "Liverpool", Country = countries[0] },
                };

                context.Cities.AddRange(cities);

                var competitions = new List<Competition>
                {
                    new Competition { Name = "Premier League", Type = CompetitionType.League, Country = countries[0] },
                    new Competition { Name = "Bundesliga", Type = CompetitionType.League, Country = countries[1] },
                    new Competition { Name = "Super Liga", Type = CompetitionType.League, Country = countries[2] },
                    new Competition { Name = "La Liga", Type = CompetitionType.League, Country = countries[3] },
                    new Competition { Name = "UEFA Champions League", Type = CompetitionType.Cup, Country = countries[4] },
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
                    new Season { Start = new DateTime(1992, 9, 12), End = new DateTime(2008, 5, 14), Competition = competitions[4] },
                };

                context.Seasons.AddRange(seasons);

                var clubs = new List<Club>
                {
                    new Club { Name = "F.C. Barcelona", Venue = "Camp Nou", YearFounded = 1899, City = cities[4], Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/2017.png" },
                    new Club { Name = "Real Madrid C.F.", Venue = "Santiago Bernabéu", YearFounded = 1899, City = cities[5], Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/2016.png" },
                    new Club { Name = "Liverpool", Venue = "Anfield", YearFounded = 1899, City = cities[6], Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/663.png" },
                    new Club { Name = "Manchester United", Venue = "Old Trafford", City = cities[1], YearFounded = 1899, Crest = "https://secure.cache.images.core.optasports.com/soccer/teams/150x150/662.png" },
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

                List<DateTime> dates_a = new List<DateTime>();
                List<DateTime> dates_b = new List<DateTime>();

                for (int i = 0; i < 10; i++) dates_a.Add(new DateTime(2017+i, 10, 5, 20, 45, 0));
                for (int i = 0; i < 10; i++) dates_b.Add(new DateTime(2017+i, 10, 5, 18, 15, 0));

                var matches = new List<Match> 
                {
                    new Match { KickOff = dates_a[0], GameWeek = 1, HomeTeam = clubs[0], AwayTeam = clubs[1], Season = seasons[0], },
                    new Match { KickOff = dates_a[1], GameWeek = 2, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[3], },
                    new Match { KickOff = dates_a[0], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[4], },
                    new Match { KickOff = dates_a[0], GameWeek = 1, HomeTeam = clubs[0], AwayTeam = clubs[1], Season = seasons[5], },
                    new Match { KickOff = dates_a[0], GameWeek = 1, HomeTeam = clubs[0], AwayTeam = clubs[1], Season = seasons[0], },
                    new Match { KickOff = dates_a[5], GameWeek = 2, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[3], },
                    new Match { KickOff = dates_a[6], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[1], },
                    new Match { KickOff = dates_a[7], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[2], },
                    new Match { KickOff = dates_a[8], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[4], },
                    new Match { KickOff = dates_a[9], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[3], },
                    new Match { KickOff = dates_b[0], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[4], },
                    new Match { KickOff = dates_b[1], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[5], },
                    new Match { KickOff = dates_b[2], GameWeek = 1, HomeTeam = clubs[0], AwayTeam = clubs[1], Season = seasons[5], },
                    new Match { KickOff = dates_b[3], GameWeek = 1, HomeTeam = clubs[0], AwayTeam = clubs[1], Season = seasons[0], },
                    new Match { KickOff = dates_b[4], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[3], },
                    new Match { KickOff = dates_b[5], GameWeek = 3, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[3], },
                    new Match { KickOff = dates_b[0], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[0], },
                    new Match { KickOff = dates_b[7], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[2], },
                    new Match { KickOff = dates_b[8], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[1], },
                    new Match { KickOff = dates_b[9], GameWeek = 1, HomeTeam = clubs[1], AwayTeam = clubs[0], Season = seasons[3], },
                };

                context.Matches.AddRange(matches);

                var matchPlayers = new List<MatchPlayer>
                {
                    new MatchPlayer
                    {
                        IsHome = true,
                        IsSubstitute = false,
                        ShirtNumber = 10,
                        Player = players[0],
                        Match = matches[0],
                    },
                    new MatchPlayer
                    {
                        IsHome = true,
                        IsSubstitute = false,
                        ShirtNumber = 17,
                        Player = players[1],
                        Match = matches[0],
                    },
                    new MatchPlayer
                    {
                        IsHome = false,
                        IsSubstitute = false,
                        ShirtNumber = 9,
                        Player = players[2],
                        Match = matches[0],
                    },
                    new MatchPlayer
                    {
                        IsHome = true,
                        IsSubstitute = true,
                        ShirtNumber = 99,
                        Player = players[0],
                        Match = matches[0],
                    },
                };

                var incidents = new List<Incident>
                {
                    new Incident
                    {
                        HomeScore = 1,
                        AwayScore = 0,
                        Minute = 45,
                        Type = IncidentType.Period,
                        Class = IncidentClass.HT,
                        IsHome = true,
                        Match = matches[0],
                    },
                    new Incident
                    {
                        HomeScore = 2,
                        AwayScore = 1,
                        Minute = 90,
                        Type = IncidentType.Period,
                        Class = IncidentClass.FT,
                        IsHome = true,
                        Match = matches[0],
                    },
                    new Incident
                    {
                        HomeScore = 1,
                        AwayScore = 0,
                        Minute = 27,
                        Type = IncidentType.Goal,
                        Class = IncidentClass.None,
                        IsHome = true,
                        PrimaryPlayer = matchPlayers[0],
                        SecondaryPlayer = matchPlayers[1],
                        Match = matches[0],
                    },
                    new Incident
                    {
                        HomeScore = 1,
                        AwayScore = 1,
                        Minute = 54,
                        Type = IncidentType.Goal,
                        Class = IncidentClass.None,
                        IsHome = false,
                        PrimaryPlayer = matchPlayers[2],
                        Match = matches[0],
                    },
                    new Incident
                    {
                        HomeScore = 2,
                        AwayScore = 1,
                        Minute = 74,
                        Type = IncidentType.Goal,
                        Class = IncidentClass.Penalty,
                        IsHome = true,
                        PrimaryPlayer = matchPlayers[0],
                        SecondaryPlayer = matchPlayers[1],
                        Match = matches[0],
                    },
                };

                context.Incidents.AddRange(incidents);

                context.MatchPlayers.AddRange(matchPlayers);

                await context.SaveChangesAsync();
            }
        }
    }
}
