using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripWMe.Models
{
    public class TripModel
    {
        [Required]
        public string Name { get; set; }
        public double StarRating { get; set; }

        public List<StopModel> Stops {get;set;}

        public TripStatistics TripStatistics { get; set; }
    }
}
