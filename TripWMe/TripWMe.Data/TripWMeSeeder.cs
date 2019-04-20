using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<TUser> _userManager;

        public TripWMeSeeder(TripWMeContext context, UserManager<TUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task Seed(string recreateDbOption)
        {
            if (recreateDbOption != "True")
            {
                return;
            }
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();

            var user1 = new TUser() { FirstName = "John", LastName = "Smith", Email = "john.smith@gmail.com", UserName = "john.smith@gmail.com" };
            var user1result = await _userManager.CreateAsync(user1, "P@ssw0rd!");
            if (user1result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Not enable to create user1");
            }
            var user2 = new TUser() { FirstName = "Mark", LastName = "Smith", Email = "mark.smith@gmail.com", UserName = "mark.smith@gmail.com" };
            var user2result = await _userManager.CreateAsync(user2, "P@ssw0rd!");
            if (user2result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Not enable to create user2");
            }

            var country1 = new Country() { Name = "Poland", Alpha2Code = "PL" };
            var country2 = new Country() { Name = "Germany", Alpha2Code = "GE" };
            var country3 = new Country() { Name = "UK", Alpha2Code = "GB" };

            var locationType1 = new LocationType() { Name = LocationType.LocType.Drink };
            var locationType2 = new LocationType() { Name = LocationType.LocType.WonderOfWorld };

            var location1 = new Location() { Name = "Location 1", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 1", Country = country1, LocationType = locationType1 };
            var location2 = new Location() { Name = "Location 2", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 2", Country = country1, LocationType = locationType2 };
            var location3 = new Location() { Name = "Location 3", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 3", Country = country2, LocationType = locationType1 };
            var location4 = new Location() { Name = "Location 4", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 4", Country = country3, LocationType = locationType2 };

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
                    },
                    UserTrips = new List<UserTrip>()
                    {
                        new UserTrip() { TUser = user1, IsOrganiser = true},
                        new UserTrip() { TUser = user2, IsOrganiser = false }
                    }

                },
                new Trip()
                {
                    Name = "Trip 2",
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
                    },
                    UserTrips = new List<UserTrip>()
                    {
                        new UserTrip() { TUser = user1, IsOrganiser = true},
                        new UserTrip() { TUser = user2, IsOrganiser = false }
                    }
                },
                new Trip()
                {
                    Name = "Trip 3",
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
                    },
                    UserTrips = new List<UserTrip>()
                    {
                        new UserTrip() { TUser = user1, IsOrganiser = true},
                        new UserTrip() { TUser = user2, IsOrganiser = false }
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