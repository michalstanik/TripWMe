using System.Collections.Generic;
using TripWMe.Models.Stops;
using TripWMe.Models.User;

namespace TripWMe.Models.Trips
{
    public class TripWithStopsAndUsersModel : TripWithStopsModel
    {
        
        public List<TUserModel> Users { get; set; } = new List<TUserModel>();
    }
}
