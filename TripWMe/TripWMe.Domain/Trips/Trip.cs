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
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TripCode { get; set; }
        public double StarRating { get; set; }

        public List<Stop> Stops { get; set; }
        public List<UserTrip> UserTrips { get; set; }

        public TripStats TripStats { get; set; }

        public List<TUser> Users()
        {
            var users = new List<TUser>();
            foreach (var join in UserTrips)
            {
                users.Add(join.TUser);
            }
            return users;
        }
    }

}