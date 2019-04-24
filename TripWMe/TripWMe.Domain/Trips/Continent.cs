using System.Collections.Generic;

namespace TripWMe.Domain.Trips
{
    public class Continent
    {
        public Continent()
        {
            Regions = new List<Region>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Region> Regions { get; set; }
    }
}