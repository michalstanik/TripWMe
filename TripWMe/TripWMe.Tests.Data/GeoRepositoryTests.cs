using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Data;
using TripWMe.Data.Repositories;
using TripWMe.Domain.GeoEntities;
using TripWMe.Domain.Stops;
using TripWMe.Domain.Trips;
using static TripWMe.Data.SampleData.CountriesModel;

namespace TripWMe.Tests.Data
{
    [TestClass]
    public class GeoRepositoryTests
    {
        private DbContextOptions<TripWMeContext> _options;
        private TripWMeContext _context;

        public GeoRepositoryTests()
        {

            _options = new DbContextOptionsBuilder<TripWMeContext>()
                .UseInMemoryDatabase(databaseName: "TripWMeInMemory").Options;
            _context = new TripWMeContext(_options);
        }

        [TestMethod]
        public async Task GetCountriesForTripRetriewCountryList()
        {
            SeedInMemoryStore();
            var repo = new GeoEntitiesRepository(_context);

            var resultsFromDb =  await repo.GetCountriesForTrip(2);

            Assert.AreEqual(resultsFromDb.Count(), 2);
        }

        private async void SeedInMemoryStore()
        {
            SeedCountries();

            var country1 = _context.Country.Where(c => c.Alpha3Code == "AZE").FirstOrDefault();
            var country2 = _context.Country.Where(c => c.Alpha3Code == "MEX").FirstOrDefault();
            var countryThailand = _context.Country.Where(c => c.Alpha3Code == "THA").FirstOrDefault();


            var locationTypeDrink = new LocationType() { Name = LocationType.LocType.Drink };
            var locationTypeWonderOdWorld = new LocationType() { Name = LocationType.LocType.WonderOfWorld };

            var location1 = new Location() { Name = "Location 1", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 1", Country = country1, LocationType = locationTypeDrink };
            var location2 = new Location() { Name = "Location 2", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 2", Country = country1, LocationType = locationTypeWonderOdWorld };
            var location3 = new Location() { Name = "Location 3", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 3", Country = country2, LocationType = locationTypeWonderOdWorld };
            var location4 = new Location() { Name = "Location 4", Latitude = 5435.4554, Longitude = 4535.6542, Description = "Description 4", Country = country2, LocationType = locationTypeDrink };

            _context.Trip.AddRange(
               new Trip()
               {
                   Name = "My First Asian Trip",
                   Stops = new List<Stop>()
                   {
                        new Stop()
                        {
                            Location = new Location(){
                                Country = countryThailand,
                                Latitude = 13.75,
                                Longitude = 100.516667,
                                Name = "Tai Hotel",
                                Description = "Hotel on Bankgok superb",
                                LocationType = locationTypeDrink
                            },
                            Departure = DateTime.Today.AddDays(10),
                            Arrival = DateTime.Today.AddDays(5),
                            Order = 1,
                            StopDescription = "Hotel on Bankgok superb",
                            StopName = "Tai Hotel"
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
                   }
               }
               );
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

                    if (existingRegion == null)
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
