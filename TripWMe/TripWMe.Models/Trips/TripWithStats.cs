using System.ComponentModel.DataAnnotations;

namespace TripWMe.Models.Trips
{
    public class TripWithStats
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string TripCode { get; set; }

        public TripStatsModel TripStats { get; set; }
    }
}
