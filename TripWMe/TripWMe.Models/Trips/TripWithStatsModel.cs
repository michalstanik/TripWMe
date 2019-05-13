using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripWMe.Models.GeoEntities;

namespace TripWMe.Models.Trips
{
    public class TripWithStatsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TripCode { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public TripStatsModel TripStats { get; set; }
        public List<string> CountryCodes { get; set; }
    }
}
