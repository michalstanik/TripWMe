using System.Collections.Generic;
using System.Threading.Tasks;
using TripWMe.Domain.GeoEntities;
using TripWMe.Domain.User;

namespace TripWMe.Data.RepositoryInterfaces
{
    public interface IGeoEntitiesRepository
    {
        Task<ICollection<Country>> GetCountriesForTrip(int tripID);
        Task<ICollection<Country>> GetCountriesForAllTrips();
        Task<Dictionary<string, long>> GetCountireAssesmentForUser(TUser user);
    }
}
