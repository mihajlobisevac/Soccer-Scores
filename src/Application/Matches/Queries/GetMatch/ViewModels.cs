using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Matches.Queries.GetMatch
{
    public class ClubViewModel : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
    }

    public class PlayerViewModel : IMapFrom<Player>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class MatchPlayerViewModel : IMapFrom<MatchPlayer>
    {
        public int Id { get; set; }
        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public int ShirtNumber { get; set; }
        public PlayerViewModel Player { get; set; }
    }

    public class IncidentViewModel : IMapFrom<Incident>
    {
        public int Id { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool IsHome { get; set; }
        public MatchPlayerViewModel PrimaryPlayer { get; set; }
        public MatchPlayerViewModel SecondaryPlayer { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Incident, IncidentViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class.ToString()));
        }
    }

    public class SeasonViewModel : IMapFrom<Season>
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonViewModel>()
                .ForMember(dest => dest.CompetitionId, opt => opt.MapFrom(src => src.Competition.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Competition.Type));
        }
    }
}
