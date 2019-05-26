using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Models.GeoEntities;

namespace TripWMe.App.Controllers
{
    [Route("api/geo/")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IGeoEntitiesRepository _geoRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public GeoController(IGeoEntitiesRepository geoRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _geoRepository = geoRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("GetCountriesForAllTrips", Name = "GetCountriesForAllTrips")]
        public async Task<ActionResult<List<CountryModel>>> GetCountriesForAllTrips()
        {
            try
            {
                var results = await _geoRepository.GetCountriesForAllTrips();
                var mapped = _mapper.Map<List<CountryModel>>(results);
                return mapped;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetCountriesForTrip/{tripId}", Name = "GetCountriesForTrip")]
        public async Task<ActionResult<List<CountryModel>>> GetCountriesForTrip(int tripId)
        {
            try
            {
                var results = await _geoRepository.GetCountriesForTrip(tripId);
                var mapped = _mapper.Map<List<CountryModel>>(results);
                return mapped;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}