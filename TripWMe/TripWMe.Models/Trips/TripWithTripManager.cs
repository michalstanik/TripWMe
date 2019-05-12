using TripWMe.Models.User;

namespace TripWMe.Models.Trips
{
    public class TripWithTripManager : TripModel 
    {
        public TUserModel TripManager { get; set; }
    }
}
