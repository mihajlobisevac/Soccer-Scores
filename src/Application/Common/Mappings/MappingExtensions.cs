using SoccerScores.Application.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerScores.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<T>> PaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
            => PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);
    }
}
