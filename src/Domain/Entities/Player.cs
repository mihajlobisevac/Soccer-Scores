using Domain.Enums;
using System;

namespace SoccerScores.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Position Position { get; set; }
        public Foot Foot { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public Country Nationality { get; set; }
        public City PlaceOfBirth { get; set; }
    }
}
