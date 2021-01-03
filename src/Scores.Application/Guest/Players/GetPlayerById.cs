using Scores.Application.Guest.Cities;
using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Countries;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Players
{
    [Service]
    public class GetPlayerById
    {
        private readonly IPlayerManager playerManager;
        private readonly IClubManager clubManager;
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;

        public GetPlayerById(IPlayerManager playerManager, IClubManager clubManager, IVenueManager venueManager, 
            ICityManager cityManager, ICountryManager countryManager)
        {
            this.playerManager = playerManager;
            this.clubManager = clubManager;
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Position { get; set; }
            public int ShirtNumber { get; set; }
            public DateTime DoB { get; set; }
            public string Foot { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public GetClubById.Response Club { get; set; }
            public GetCountryById.Response Nationality { get; set; }
            public GetCityById.Response CityOfBirth { get; set; }
        }

        public Response Do(int id)
        {
            var getClub = new GetClubById(clubManager, venueManager, cityManager, countryManager);
            var getNationality = new GetCountryById(countryManager);
            var getCity = new GetCityById(cityManager, countryManager);

            return playerManager.GetPlayerById(id, (player)
                => new Response
                {
                    Id = player.Id,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Position = player.Position,
                    ShirtNumber = player.ShirtNumber,
                    DoB = player.DoB,
                    Foot = player.Foot,
                    Height = player.Height,
                    Weight = player.Weight,
                    Club = getClub.Do(player.ClubId),
                    Nationality = getNationality.Do(player.NationalityCountryId),
                    CityOfBirth = getCity.Do(player.PoBCityId),
                });
        }
    }
}
