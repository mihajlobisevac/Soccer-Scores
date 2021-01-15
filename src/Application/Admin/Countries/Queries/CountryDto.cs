using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Countries.Queries
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Flag { get; set; }
        public string Test { get; set; }

        public ICollection<string> Cities { get; set; }
    }
}
