using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Matches.Queries.Incidents
{
    public class IncidentDto : IMapFrom<Incident>
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool IsHome { get; set; }

        public int MatchId { get; set; }
        public int? PrimaryPlayerId { get; set; }
        public int? SecondaryPlayerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Incident, IncidentDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class.ToString()))
                .ForMember(dest => dest.PrimaryPlayerId, opt => opt.MapFrom(src => src.PrimaryPlayer.Id))
                .ForMember(dest => dest.SecondaryPlayerId, opt => opt.MapFrom(src => src.SecondaryPlayer.Id));
        }
    }
}
