using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Domain.Trips;

namespace TripWMe.Data.RepositoryInterfaces
{
    public interface ITripRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<ICollection<Trip>> GetAllTripsAsync(bool includeStops = false, bool includeUsers = false);
        Task<ICollection<Trip>> GetAllTripsWithStats();
        Task<ICollection<Trip>> GetTripsByUserAsync(string userName);
        Task<Trip> GetTripByCode(int tripCode);
        bool TripExists(int tripId);
        //Stops
        List<Stop>GetStopsForTrip(int tripId);
    }
}
