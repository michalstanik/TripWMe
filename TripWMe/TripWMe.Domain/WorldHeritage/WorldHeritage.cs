using System.Collections.Generic;

namespace TripWMe.Domain.WorldHeritage
{
    public class WorldHeritage
    {
        public WorldHeritage()
        {
            WoldHeritageCountries = new List<WorldHeritageCountry>();
        }

        public int Id { get; set; }
        public string UnescoId { get; set; }
        public string ImageUrl { get; set; }
        public string IsoCodes { get; set; }
        public string Longitude { get;set; }
        public string Latitude { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }

        public List<WorldHeritageCountry> WoldHeritageCountries { get; set; }

    }
}
