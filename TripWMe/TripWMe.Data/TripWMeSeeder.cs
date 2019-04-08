using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Domain;

namespace TripWMe.Data
{
    public class TripWMeSeeder
    {
        private readonly TripWMeContext _context;

        public TripWMeSeeder(TripWMeContext context)
        {
            _context = context;
        }
        public async Task Seed(string recreateDbOption)
        {
            if (recreateDbOption != "True")
            {
                return;
            }
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();

            var country1 = new Country() { Name = "Poland" };
            var country2 = new Country() { Name = "Germany" };
            var country3 = new Country() { Name = "UK" };

            var location1 = new Location() { LocationName = "Location 1", Latitude = 5435.4554, Longitude = 4535.6542, LocationDescription = "Description 1" , Country = country1 };
            var location2 = new Location() { LocationName = "Location 2", Latitude = 5435.4554, Longitude = 4535.6542, LocationDescription = "Description 2" , Country = country1 };
            var location3 = new Location() { LocationName = "Location 3", Latitude = 5435.4554, Longitude = 4535.6542, LocationDescription = "Description 3" , Country = country2 };
            var location4 = new Location() { LocationName = "Location 4", Latitude = 5435.4554, Longitude = 4535.6542, LocationDescription = "Description 4" , Country = country3 };

            _context.Trip.AddRange(
                new Trip()
                {
                    Name = "Trip 1",
                    Stops = new List<Stop>()
                    {
                        new Stop()
                        {
                            Location = location1,
                            Departure = DateTime.Today.AddDays(10),
                            Arrival = DateTime.Today.AddDays(5),
                            Order = 1,
                            StopDescription = "Stop Description 1",
                            StopName = "Stop 1"
                        },
                        new Stop()
                        {
                            Location = location2,
                            Departure = DateTime.Today.AddDays(10),
                            Arrival = DateTime.Today.AddDays(5),
                            Order = 2,
                            StopDescription = "Stop Description 2",
                            StopName = "Stop 2"
                        }
                    }
                }
                );
            await _context.SaveChangesAsync();

            var existingTrip = _context.Trip.Where(t => t.Id == 1).FirstOrDefault();
            if (existingTrip != null)
            {
                existingTrip.Name = "Changed Name";
                existingTrip.StarRating = 5.5;
            }

            await _context.SaveChangesAsync();

        }
    }
}