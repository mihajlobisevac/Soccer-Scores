using System;

namespace Scores.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Position { get; set; }
        public int ShirtNumber { get; set; }
        public string Foot { get; set; }

        public int ClubId { get; set; }
        public int NationalityCountryId { get; set; }
        public int PoBCityId { get; set; }
    }
}
