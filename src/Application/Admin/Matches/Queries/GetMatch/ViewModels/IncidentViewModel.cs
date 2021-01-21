using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Admin.Matches.Queries.GetMatch.ViewModels
{
    public class IncidentViewModel : IMapFrom<Incident>
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool IsHome { get; set; }
        public PlayerViewModel PrimaryPlayer { get; set; }
        public PlayerViewModel SecondaryPlayer { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Incident, IncidentViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class.ToString()))
                .ForMember(dest => dest.PrimaryPlayer, opt => opt.MapFrom(src => src.PrimaryPlayer))
                .ForMember(dest => dest.SecondaryPlayer, opt => opt.MapFrom(src => src.SecondaryPlayer));
        }
    }
}
