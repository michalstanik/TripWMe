using System.Collections.Generic;
using TripWMe.Domain.GeoEntities;

namespace TripWMe.Domain.GeoEntities
{
    public class LocationType
    {
        public LocationType()
        {
            Locations = new List<Location>();
        }

        public enum LocType
        {
            Food, 
            Drink, 
            Sleep,
            WonderOfWorld

        }

        public int Id { get; set; }
        public LocType Name { get; set; }

        public List<Location> Locations { get; set; }


    }
}
