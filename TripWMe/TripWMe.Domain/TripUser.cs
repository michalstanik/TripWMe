
using Microsoft.AspNetCore.Identity;



namespace TripWMe.Domain
{
    public class TripUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}