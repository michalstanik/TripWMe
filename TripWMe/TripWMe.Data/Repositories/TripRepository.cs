using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain;

namespace TripWMe.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripWMeContext _context;
        private readonly ILogger<TripRepository> _logger;

        public TripRepository(TripWMeContext context, ILogger<TripRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public async Task<ICollection<Trip>> GetAllTripsAsync(bool includeStops = false, bool includeUsers = false)
        {
            _logger.LogInformation($"Getting all Projects");

            IQueryable<Trip> query = _context.Trip;

            if (includeStops && !includeUsers)
            {
                query = query
                    .Include(c => c.Stops)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(c => c.Country)
                    .Include(c => c.Stops)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(lt => lt.LocationType);
            }
            else if (!includeStops && includeUsers)
            {
                query = query.Include(c => c.UserTrips)
                    .ThenInclude(pc => pc.TUser);
            }
            else if (includeStops && includeUsers)
            {
                query = query.Include(i => i.Stops)
                    .ThenInclude(l => l.Location)
                    .ThenInclude(c => c.Country)
                    .Include(c => c.UserTrips)
                    .ThenInclude(pc => pc.TUser);
            }

            _logger.LogInformation($"Getting all Projects. Returned: {query.Count()}");

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
