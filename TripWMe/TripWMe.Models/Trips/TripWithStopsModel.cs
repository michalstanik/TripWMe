using System.Collections.Generic;
using TripWMe.Models.Stops;

namespace TripWMe.Models.Trips
{
    public class TripWithStopsModel : TripModel
    {
        public List<StopModel> Stops { get; set; } = new List<StopModel>();
    }
}
