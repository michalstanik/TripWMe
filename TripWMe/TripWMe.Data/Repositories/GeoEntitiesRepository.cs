using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.GeoEntities;
using TripWMe.Domain.Trips;
using TripWMe.Domain.User;

namespace TripWMe.Data.Repositories
{
    public class GeoEntitiesRepository : IGeoEntitiesRepository
    {
        private readonly TripWMeContext _context;
        private readonly ILogger<GeoEntitiesRepository> _logger;
        private readonly UserManager<TUser> _userManager;

        public GeoEntitiesRepository(TripWMeContext context)
        {
            _context = context;
        }
        public GeoEntitiesRepository(TripWMeContext context, ILogger<GeoEntitiesRepository> logger, UserManager<TUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<ICollection<Country>> GetCountriesForAllTrips()
        {
            var query = _context.Trip;

            var results = await ReturnCountriesBasedOnTripQuery(query);

            return results;
        }

        public async Task<ICollection<Country>> GetCountriesForTrip(int tripId)
        {
            var query = _context.Trip.Where(t => t.Id == tripId);

            var results = await ReturnCountriesBasedOnTripQuery(query);

            return results;
        }

        private async Task<ICollection<Country>> ReturnCountriesBasedOnTripQuery(IQueryable<Trip> inputQuery)
        {
            var listOfTripsWithGraph = inputQuery.Include(i => i.Stops)
                    .ThenInclude(l => l.Location)
                    .ThenInclude(c => c.Country)
                    .ThenInclude(r => r.Region)
                .Include(c => c.Stops)
                    .ThenInclude(l => l.Location);

            await listOfTripsWithGraph.ToListAsync();

            var listOfCountriesToReturn = new List<Country>();

            foreach (var trip in listOfTripsWithGraph)
            {
                var listOfCountries = trip.Stops
                    .Where(s => s.Location != null)
                    .Select(c => c.Location)
                    .Where(v => v.CountryId != null)
                    .Select(c => c.Country).Distinct().ToList();

                foreach (var country in listOfCountries)
                {
                    if (listOfCountriesToReturn.Where(c => c.Id == country.Id).Count() == 0)
                    {
                        listOfCountriesToReturn.Add(country);
                    }
                }
            }

            return listOfCountriesToReturn;
        }
    }
}
