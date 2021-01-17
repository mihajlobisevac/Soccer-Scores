using System;
using System.Collections.Generic;

namespace SoccerScores.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Flag { get; set; }

        public ICollection<City> Cities { get; set; }

        public object Select(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
