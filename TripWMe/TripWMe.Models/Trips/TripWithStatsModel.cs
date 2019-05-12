using System.ComponentModel.DataAnnotations;

namespace TripWMe.Models.Trips
{
    public class TripWithStatsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TripCode { get; set; }

        public TripStatsModel TripStats { get; set; }
    }
}
