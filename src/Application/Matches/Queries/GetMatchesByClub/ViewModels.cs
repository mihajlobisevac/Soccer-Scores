using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByClub
{
    public class ResultViewModel : IMapFrom<Incident>
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }

    public class ClubViewModel : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
    }

    public class SeasonViewModel : IMapFrom<Season>
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonViewModel>()
                .ForMember(dest => dest.CompetitionId, opt => opt.MapFrom(src => src.Competition.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name));
        }
    }
}
