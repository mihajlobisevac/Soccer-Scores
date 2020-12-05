using Scores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface ICityManager
    {
        Task<int> CreateCity(City city);
        Task<int> DeleteCity(int id);
        Task<int> UpdateCity(City city);

        TResult GetCityById<TResult>(int id, Func<City, TResult> selector);
    }
}
