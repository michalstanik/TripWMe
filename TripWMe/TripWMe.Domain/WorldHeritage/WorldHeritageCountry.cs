using TripWMe.Domain.Trips;

namespace TripWMe.Domain.WorldHeritage
{
    public class WorldHeritageCountry
    {
        public int WorldHeritageId { get; set; }
        public WorldHeritage WorldHeritage { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
