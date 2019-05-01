using System.Collections.Generic;
using TripWMe.Domain.User;

namespace TripWMe.Domain.Trips
{
    public class Trip
    {
        public Trip()
        {
            Stops = new List<Stop>();
            TripStats = new TripStats();
            UserTrips = new List<UserTrip>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TripCode { get; set; }
        public double StarRating { get; set; }

        public List<Stop> Stops { get; set; }
        public List<UserTrip> UserTrips { get; set; }

        public TripStats TripStats { get; set; }

        public IEnumerable<TUser> Users()
        {
            var users = new List<TUser>();
            foreach (var join in UserTrips)
            {
                users.Add(join.TUser);
            }
            return users;
        }

        public bool UserExist (TUser user)
        {
            foreach (var join in UserTrips)
            {
                if (join.TUser == user) return true;
            }
            return false;
        }
    }

}