using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.Trips;
using TripWMe.Models;
using TripWMe.Models.Trips;

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
        public async Task<ActionResult<List<TripWithStats>>> GetAllTripsWithStats()
        {
            try
            {
                var results = await _repository.GetAllTripsWithStats();
                var mapped = _mapper.Map<List<TripWithStats>>(results);
                return mapped;
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

        [HttpGet("GetTrip/{tripCode}", Name = "GetTrip")]
        public async Task<ActionResult<TripModel>> GetTrip(int tripCode)
        {
            try
            {
                var result = await _repository.GetTripByCode(tripCode);
                var mapped = _mapper.Map<TripModel>(result);
                return mapped;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
        public async Task<IActionResult> CreateTrip([FromBody] TripForCreationModel trip)
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

            return CreatedAtRoute("GetTrip", new { id = tripToReturn.Id }, tripToReturn);
        }

    }
}
