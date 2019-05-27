using TripWMe.Domain.GeoEntities;

namespace TripWMe.Domain.User
{
    public class UserCountryAssessment
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string TUserId { get; set; }
        public TUser TUser { get; set; }

        public long AreaLevelAssessment { get; set; }

    }
}
