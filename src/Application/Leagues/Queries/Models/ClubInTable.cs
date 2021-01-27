using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Leagues.Queries.Models
{
    public class ClubInTable : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int Points { get; set; }
    }
}
