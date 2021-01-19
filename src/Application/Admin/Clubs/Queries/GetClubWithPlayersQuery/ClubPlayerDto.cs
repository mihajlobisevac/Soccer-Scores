using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Admin.Clubs.Queries.GetClubWithPlayersQuery
{
    public class ClubPlayerDto : IMapFrom<ClubPlayer>
    {
        public int Id { get; set; }

        public int ShirtNumber { get; set; }

        public PlayerDto Player { get; set; }
    }
}
