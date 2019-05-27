
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TripWMe.Domain.Trips;

namespace TripWMe.Domain.User
{
    public class TUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserTrip> UserTrips { get; set; }
        public List<UserCountryAssessment> UserCountryAssessments { get; set; }
        public List<Trip> TripsForManager { get; set; }
    }
}