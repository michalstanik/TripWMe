﻿using System.Collections.Generic;
using TripWMe.CoreHelpers.Attributes;

namespace TripWMe.Domain
{
    [Auditable]
    public class Trip
    {
        public Trip()
        {
            Stops = new List<Stop>();
        }
        public int Id { get; set; }
        [Auditable]
        public string Name { get; set; }
        public double StarRating { get; set; }

        public List<Stop> Stops { get; set; }
        public List<UserTrip> UserTrips { get; set; }
    }
}
