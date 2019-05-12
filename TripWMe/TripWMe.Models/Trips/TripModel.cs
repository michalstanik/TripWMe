using System.Collections.Generic;
using TripWMe.Models.Stops;
using TripWMe.Models.User;

namespace TripWMe.Models.Trips
{
    public class TripModel : TripAbstractBase
    {
        public int Id { get; set; }

        public List<StopModel> Stops {get;set;}
        public List<TUserModel> Users { get; set; }
    }
}
