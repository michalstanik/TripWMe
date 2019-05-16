using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Domain.Stops;
using TripWMe.Domain.Trips;

namespace TripWMe.Data.RepositoryInterfaces
{
    public interface ITripRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<ICollection<Trip>> GetAllTripsAsync(bool includeStops = false, bool includeUsers = false);
        Task<ICollection<Trip>> GetTripsByUserAsync(string userName);
        Task<Trip> GetTrip(int tripId, bool includeStops = false, bool includeUsers = false);
        bool TripExists(int tripId);
        IEnumerable<Trip> GetTrips(IEnumerable<int> tripIds);
        void DeleteTrip(Trip trip);

        //Stops
        List<Stop>GetStopsForTrip(int tripId);
        Stop GetStopForTrip(int tripId, int stopId);
        void DeleteStop(Stop stop);
        void UpdateStopForTrip(Stop stop);
    }
}
