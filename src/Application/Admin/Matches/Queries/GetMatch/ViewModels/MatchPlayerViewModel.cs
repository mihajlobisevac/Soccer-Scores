using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Admin.Matches.Queries.GetMatch.ViewModels
{
    public class MatchPlayerViewModel : IMapFrom<MatchPlayer>
    {
        public int Id { get; set; }
        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public PlayerViewModel Player { get; set; }
    }
}
