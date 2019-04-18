using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWMe.Models
{
    public class TripWithStats
    {
        [Required]
        public string Name { get; set; }
        public string TripCode { get; set; }

        public TripStatsModel TripStats { get; set; }
    }
}
