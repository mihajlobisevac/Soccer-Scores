using System;
using System.Threading.Tasks;
using Scores.Domain.Models;

namespace Scores.Domain.Infrastructure
{
    public interface ICountryManager
    {
        Task<int> CreateCountry(Country country);
        Task<int> DeleteCountry(int id);
        Task<int> UpdateCountry(Country country);

        TResult GetCountryById<TResult>(int id, Func<Country, TResult> selector);
    }
}
