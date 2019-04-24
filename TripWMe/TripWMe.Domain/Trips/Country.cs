using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TripWMe.Domain.Trips
{

    public class Country
    {
        public Country()
        {
            Locations = new List<Location>();
        }
        public int Id { get;set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public long Area { get; set; }
        public List<Location> Locations { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }
    }
}