using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TripWMe.Domain
{

    public class Country
    {
        public Country()
        {
            Locations = new List<Location>();
        }
        public int Id { get;set; }
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public List<Location> Locations { get; set; }
        
    }
}