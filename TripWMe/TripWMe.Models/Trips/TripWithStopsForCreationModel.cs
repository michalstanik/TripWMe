using System.Collections.Generic;
using TripWMe.Models.Stops;

namespace TripWMe.Models.Trips
{
    public class TripWithStopsForCreationModel : TripAbstractBase
    {
        public List<StopForCreationModel> Stops { get; set; } = new List<StopForCreationModel>();
    }
}
