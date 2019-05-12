using TripWMe.Models.User;

namespace TripWMe.Models.Trips
{
    public class TripWithTripManagerForCreation : TripForCreationModel
    {
        public TUserModel TripManager { get; set; }
    }
}
