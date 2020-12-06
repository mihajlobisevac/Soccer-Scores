using System;
using System.Linq;
using System.Threading.Tasks;
using Scores.Domain.Infrastructure;
using Scores.Domain.Models;

namespace Scores.Database
{
    public class CountryManager : ICountryManager
    {
        private readonly AppDbContext context;

        public CountryManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateCountry(Country country)
        {
            context.Countries.Add(country);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteCountry(int id)
        {
            var country = context.Countries.FirstOrDefault(x => x.Id == id);

            context.Countries.Remove(country);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateCountry(Country country)
        {
            context.Countries.Update(country);

            return context.SaveChangesAsync();
        }

        public TResult GetCountryById<TResult>(int id, Func<Country, TResult> selector)
            => context.Countries
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
    }
}
