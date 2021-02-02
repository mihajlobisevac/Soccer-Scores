using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Matches.Queries.GetMatch.Models
{
    public class MatchPlayerVm : IMapFrom<MatchPlayer>
    {
        public int Id { get; set; }
        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public int ShirtNumber { get; set; }
        public PlayerVm Player { get; set; }
    }
}
