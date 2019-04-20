using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWMe.Domain;

namespace TripWMe.Data.RepositoryInterfaces
{
    public interface ITripRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<ICollection<Trip>> GetAllTripsAsync(bool includeStops = false, bool includeUsers = false);
        Task<ICollection<Trip>> GetAllTripsWithStats();
        Task<Trip> GetTripByCode(string tripCode);
    }
}
