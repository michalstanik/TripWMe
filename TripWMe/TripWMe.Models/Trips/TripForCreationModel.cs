using System.Collections.Generic;
using TripWMe.Models.Stops;

namespace TripWMe.Models.Trips
{
    public class TripForCreationModel
    {
        public string Name { get; set; }
        public string TripCode { get; set; }
        public double StarRating { get; set; }

        public List<StopForCreationModel> Stops { get; set; }
            = new List<StopForCreationModel>();
    }
}
