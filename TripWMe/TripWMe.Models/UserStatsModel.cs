using System.Collections.Generic;

namespace TripWMe.Models
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
