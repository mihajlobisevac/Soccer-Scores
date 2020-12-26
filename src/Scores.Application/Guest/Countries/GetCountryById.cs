using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Countries
{
    [Service]
    public class GetCountryById
    {
        private readonly ICountryManager countryManager;

        public GetCountryById(ICountryManager countryManager)
        {
            this.countryManager = countryManager;
        }

        public class Response
        {
            public string Name { get; set; }
            public string NameCode { get; set; }
            public string Flag { get; set; }
        }

        public Response Do(int id) =>
            countryManager.GetCountryById(id, x => new Response
            {
                Name = x.Name,
                NameCode = x.NameCode,
                Flag = x.Flag,
            });
    }
}
