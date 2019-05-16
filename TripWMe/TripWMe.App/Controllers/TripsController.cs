using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.CoreHelpers.Attributes;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.Trips;
using TripWMe.Models.GeoEntities;
using TripWMe.Models.Trips;
using TripWMe.Models.User;

namespace TripWMe.App.Controllers
{
    [Route("api/trips/")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public TripsController(ITripRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("GetAllTripsWithStats", Name = "GetAllTripsWithStats")]
        public async Task<ActionResult<List<TripWithStatsModel>>> GetAllTripsWithStats()
        {
            try
            {
                var includeStops = true;
                var includeUsers = true;

                var resultsFromRepo = await _repository.GetAllTripsAsync(includeStops, includeUsers);

                var resultsToBeReturned = new List<TripWithStatsModel>();

                foreach (var item in resultsFromRepo)
                {
                    var minDate = new DateTime();
                    var maxDate  = new DateTime();

                    if (item.Stops.Count != 0)
                    {
                        minDate = item.Stops.Select(d => d.Arrival).Min().Date;
                        maxDate = item.Stops.Select(d => d.Arrival).Max().Date;
                    }

                    var newItem = new TripWithStatsModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        TripCode = item.TripCode,
                        TripStats = new TripStatsModel()
                        {
                            StopCount = item.Stops.Count,
                            CountryCount = item.Stops.Where(s => s.Location != null).Select(c => c.Location.CountryId).Distinct().Count(),
                            LocationCount = item.Stops.Where(s => s.Location != null).Select(c => c.Location.Id).Distinct().Count(),
                            UserCount = item.UserTrips.Where(t => t.TUser != null).Select(u => u.TUserId).Distinct().Count()
                        },
                        CountryCodes = item.Stops
                        .Where(s => s.Location != null)
                                .Select(c => c.Location)
                                .Where(v => v.CountryId != null)
                                .Select(c => c.Country)
                                .Select(c => c.Alpha3Code).Distinct().ToList().ConvertAll(d => d.ToLower()),
                        StartDate = minDate,
                        EndDate = maxDate
                    };
                    resultsToBeReturned.Add(newItem);
                }
                return resultsToBeReturned;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAllTrips", Name = "GetAllTrips")]
        public async Task<ActionResult<List<TripModel>>> GetAllTrips(bool includeStops = false, bool includeUsers = false)
        {
            try
            {
                var results = await _repository.GetAllTripsAsync(includeStops, includeUsers);
                var mapped = _mapper.Map<List<TripModel>>(results);
                return mapped;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}", Name = "GetTrip")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.trip+json" })]
        public async Task<IActionResult> GetTrip(int id)
        {
            return await GetSpecificTrip<TripModel>(id);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.tripwithtripmanager+json" })]
        public async Task<IActionResult> GetTripWithTripManager(int id)
        {
            return await GetSpecificTrip<TripWithTripManager>(id);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.tripwithstops+json" })]
        public async Task<IActionResult> GetTripWithStops(int id)
        {
            return await GetSpecificTrip<TripWithStopsModel>(id, true, false);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.tripwithstopsandusers+json" })]
        public async Task<IActionResult> GetTripWithStopsAndUsers(int id)
        {
            return await GetSpecificTrip<TripWithStopsAndUsersModel>(id, true, true);
        }

        [HttpGet(Name = "GetUserStats")]
        public async Task<ActionResult<UserStatsModel>> GetUserStats(string userName)
        {
            try
            {
                var qry = await _repository.GetTripsByUserAsync(userName);

                var countries = new List<CountryModel>();
                int countryCount = 0;
                int tripCount = 0;

                foreach (var item in qry)
                {
                    countryCount += item.Stops.Select(s => s.Location.Country).Distinct().Count();

                    var mapped = _mapper.Map<CountryModel>(item.Stops.Select(s => s.Location.Country).Distinct().FirstOrDefault());
                    countries.Add(mapped);

                    tripCount += item.TripCode.Distinct().Count();

                }

                var result = new UserStatsModel()
                {
                    ContryCount = countryCount,

                    Countries = countries


                };
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost()]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.tripforcreation+json" })]
        public async Task<IActionResult> CreateTrip(TripForCreationModel trip)
        {
            if (trip == null)
            {
                return BadRequest();
            }

            var tripEntity = _mapper.Map<Trip>(trip);

            _repository.Add(tripEntity);

            if (!await _repository.SaveChangesAsync())
            {
                throw new Exception("Creating a trip failed on save");
            }

            var tripToReturn = _mapper.Map<TripModel>(tripEntity);


            return CreatedAtRoute("GetTrip", new { tripCode = tripToReturn.Id }, tripToReturn);
        }

        [HttpPost("{id}")]
        public IActionResult BlockTripCreation(int id)
        {
            if (_repository.TripExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var tripFromRepo = await _repository.GetTrip(id);
            if (tripFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteTrip(tripFromRepo);

            if (!await _repository.SaveChangesAsync())
            {
                throw new Exception($"Deleting a trip {id} failed on save");
            }

            return NoContent();
        }

        private async Task<IActionResult> GetSpecificTrip<T>(int tripId, bool includeStops = false, bool includeUsers = false) where T : class
        {
            var tripFromRepo = await _repository.GetTrip(tripId, includeStops, includeUsers);

            if (tripFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<T>(tripFromRepo));
        }
    }
}
