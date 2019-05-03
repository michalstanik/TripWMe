using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Models;

namespace TripWMe.App.Controllers
{
    [Route("api/trips/{tripId}/stops")]
    [ApiController]
    public class StopsController : ControllerBase
    {
        private readonly ITripRepository _repository;
        private readonly IMapper _mapper;

        public StopsController(ITripRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }       

        [HttpGet()]
        public IActionResult GetStopsForTrips(int tripId)
        {
            if(!_repository.TripExists(tripId))
            {
                NotFound();
            }

            var stopsForTripFromRepo = _repository.GetStopsForTrip(tripId);
            var stopsForTrip = _mapper.Map<List<StopModel>>(stopsForTripFromRepo);

            return Ok(stopsForTrip);
        }
    }
}
