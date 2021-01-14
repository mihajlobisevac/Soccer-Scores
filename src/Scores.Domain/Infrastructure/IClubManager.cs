using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IClubManager
    {
        Task<int> CreateClub(Club club);
        Task<int> DeleteClub(int id);
        Task<int> UpdateClub(Club club);

        TResult GetClubById<TResult>(int id, Func<Club, TResult> selector);
    }
}
