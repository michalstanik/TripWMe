using TripWMe.Domain.Trips;

namespace TripWMe.Domain.User
{
    public class UserTrip
    {
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public string TUserId { get; set; }
        public TUser TUser { get; set; }

        public bool IsOrganiser { get; set; }
        public bool IsMain { get; set; } 
    }
}
