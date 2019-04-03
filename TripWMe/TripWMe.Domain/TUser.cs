
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TripWMe.Domain
{
    public class TUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserTrip> UserTrips { get; set; }
    }
}