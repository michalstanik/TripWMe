using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Models;

namespace TripWMe.App.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet(Name = "GetAllTripsWithStats")]
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

        [HttpGet(Name = "GetAllTrips")]
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

        [HttpGet("{tripCode}")]
        [HttpGet(Name = "GetTripByCode")]
        public async Task<ActionResult<TripModel>> GetTripByCode(string tripCode)
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


    }
}
