using AutoMapper;
using Domain.Enums;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Linq;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    public class MatchDto : IMapFrom<Match>
    {
        public ClubVm HomeTeam { get; set; }
        public ClubVm AwayTeam { get; set; }

        public ResultVm Result { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchDto>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Incidents.FirstOrDefault(x => x.Class == IncidentClass.FT)));
        }
    }

    public class ClubVm : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
    }

    public class ResultVm : IMapFrom<Incident>
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
