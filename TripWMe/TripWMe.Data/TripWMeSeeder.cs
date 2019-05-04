using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Domain.Stops;
using TripWMe.Domain.Trips;
using TripWMe.Domain.User;
using static TripWMe.Data.SampleData.CountriesModel;

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

            SeedCountries();

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

            var country1 = _context.Country.Where(c => c.Alpha3Code == "AZE").FirstOrDefault();
            var country2 = _context.Country.Where(c => c.Alpha3Code == "MEX").FirstOrDefault();


            var locationType1 = new LocationType() { Name = LocationType.LocType.Drink };
            var locationType2 = new LocationType() { Name = LocationType.LocType.WonderOfWorld };

            var location1 = new Location() { Name = "Location 1", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 1", Country = country1, LocationType = locationType1 };
            var location2 = new Location() { Name = "Location 2", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 2", Country = country1, LocationType = locationType2 };
            var location3 = new Location() { Name = "Location 3", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 3", Country = country2, LocationType = locationType1 };
            var location4 = new Location() { Name = "Location 4", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 4", Country = country2, LocationType = locationType2 };

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
                            Location = location3,
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
                            Location = location2,
                            Departure = DateTime.Today.AddDays(10),
                            Arrival = DateTime.Today.AddDays(5),
                            Order = 1,
                            StopDescription = "Stop Description 1",
                            StopName = "Stop 1"
                        },
                        new Stop()
                        {
                            Location = location4,
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

        private void SeedCountries()
        {
            using (StreamReader r = new StreamReader("C:/Users/micha/source/Repository/TripWMe/TripWMe/TripWMe.Data/SampleData/countries.json"))
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                string json = r.ReadToEnd();
                List<CountriesDataModel> items = JsonConvert.DeserializeObject<List<CountriesDataModel>>(json, settings);
                foreach (var item in items)
                {
                    var continent = new Continent();
                    
                    var existingContinent = _context.Continent.Where(c => c.Name == item.region).FirstOrDefault();

                    if (existingContinent == null)
                    {
                        continent = new Continent() { Name = item.region };
                        _context.Add(continent);
                        _context.SaveChanges();
                    }
                    else
                    {
                        continent = existingContinent;
                    }

                    var region = new Region();
                    var existingRegion = _context.Region.Where(rg => rg.Name == item.subregion).FirstOrDefault();

                    if(existingRegion == null)
                    {
                        region = new Region() { Name = item.subregion, Continent = continent };
                        _context.Add(region);
                        _context.SaveChanges();
                    }
                    else
                    {
                        region = existingRegion;
                    }

                    var newCountry = new Country()
                    {
                        Name = item.name.common,
                        OfficialName = item.name.official,
                        Alpha2Code = item.cca2,
                        Alpha3Code = item.cca3,
                        Area = item.area,
                        Region = region
                    };
                    _context.Add(newCountry);
                    _context.SaveChanges();


                }
            }

        }
    }
}