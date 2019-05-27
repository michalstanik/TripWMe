using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.User;
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
        private readonly UserManager<TUser> _userManager;

        public GeoController(IGeoEntitiesRepository geoRepository, IMapper mapper, LinkGenerator linkGenerator, UserManager<TUser> userManager)
        {
            _geoRepository = geoRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _userManager = userManager;
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

        [HttpGet("GetCountriesForAllTripsWithAssessments", Name = "GetCountriesForAllTripsWithAssessments")]
        public async Task<ActionResult<List<CountryModelWithAssesments>>> GetCountriesForAllTripsWithAssessments()
        {
            try
            {
                var results = await _geoRepository.GetCountriesForAllTrips();
                var mapped = _mapper.Map<List<CountryModelWithAssesments>>(results);

                //TODO: Remove hardcoded user when authentication will be applied
                var user = await _userManager.FindByEmailAsync("john.smith@gmail.com"); 

                Dictionary<string,long> UserAssesment = await _geoRepository.GetCountireAssesmentForUser(user);

                foreach (var item in mapped)
                {
                    if(UserAssesment.ContainsKey(item.Alpha2Code))
                    {
                        item.AreaLevelAssessment = UserAssesment.GetValueOrDefault(item.Alpha2Code); 
                    }
                    else
                    {
                        item.AreaLevelAssessment = 0;
                    }
                }

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