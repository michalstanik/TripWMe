using System.Collections.Generic;

namespace TripWMe.Domain
{
    public class Location
    {
        public Location()
        {
            Stops = new List<Stop>();
        }

        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Country Country { get; set; }
        public int CountryId { get; set; }
        public List<Stop> Stops { get; set; }
  
    }
}
