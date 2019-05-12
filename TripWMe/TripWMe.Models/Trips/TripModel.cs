using System.Collections.Generic;
using TripWMe.Models.Stops;
using TripWMe.Models.User;

namespace TripWMe.Models.Trips
{
    public class TripModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TripCode { get; set; }
        public double StarRating { get; set; }
        public List<StopModel> Stops {get;set;}
        public List<TUserModel> Users { get; set; }
    }
}
