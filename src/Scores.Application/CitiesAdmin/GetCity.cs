using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.CitiesAdmin
{
    [Service]
    public class GetCity
    {
        private readonly ICityManager cityManager;

        public GetCity(ICityManager cityManager)
        {
            this.cityManager = cityManager;
        }

        public class CityViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CountryId { get; set; }
        }

        public CityViewModel Do(int id) =>
            cityManager.GetCityById(id, x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            });
    }
}
