using System.Collections.Generic;
using TripWMe.Models.GeoEntities;

namespace TripWMe.Models.User
{
    public class UserStatsModel
    {
        public UserStatsModel()
        {
            Countries = new List<CountryModel>();
        }
        public int TripCount { get; set; }
        public int ContryCount { get; set; }
        public List<CountryModel> Countries { get; set; }
    }
}
