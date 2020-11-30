using System;

namespace Scores.Domain.Models
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

        public Club Club { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
    }
}
