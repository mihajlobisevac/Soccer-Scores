using Scores.Application;
using Scores.Database;
using Scores.Domain.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() is not null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<ICityManager, CityManager>();
            @this.AddTransient<ICountryManager, CountryManager>();
            @this.AddTransient<IVenueManager, VenueManager>();
            @this.AddTransient<IClubManager, ClubManager>();
            @this.AddTransient<IMatchManager, MatchManager>();
            @this.AddTransient<IPlayerManager, PlayerManager>();
            @this.AddTransient<IEventManager, EventManager>();
            @this.AddTransient<ITournamentManager, TournamentManager>();
            @this.AddTransient<IStandingsManager, StandingsManager>();

            return @this;
        }
    }
}
