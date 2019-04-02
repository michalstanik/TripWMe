﻿using System.Collections.Generic;

namespace TripWMe.Domain
{
    public class Trip
    {
        public Trip()
        {
            Stops = new List<Stop>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public double StarRating { get; set; }

        public List<Stop> Stops { get; set; }
        public TripUser User { get; set; }
    }
}
