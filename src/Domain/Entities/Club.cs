using System.Collections.Generic;

namespace SoccerScores.Domain.Entities
{
    public class Club
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Crest { get; set; }
        public string Venue { get; set; }
        public int YearFounded { get; set; }

        public ICollection<ClubPlayer> Players { get; set; }
        public ICollection<Season> Participations { get; set; }
    }
}
