using System;

namespace TripWMe.App.Models
{
    public class StopModel
    {
        public string StopName { get; set; }
        public string StopDescription { get; set; }
        public int Order { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }

        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }

        public string LocationCountryName { get; set; }
        public string LocationCountryAlpha2Code { get; set; }
        public string LocationLocationTypeName { get; set; }
    }
}
