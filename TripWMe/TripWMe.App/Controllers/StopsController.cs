using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.Stops;
using TripWMe.Models.Stops;

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

        [HttpGet(Name = "GetStopsForTrip")]
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

        [HttpGet("{id}", Name = "GetStopForTrip")]
        public IActionResult GetStopForTrip(int tripId, int id)
        {
            if (!_repository.TripExists(tripId))
            {
                NotFound();
            }

            var stopForTripFromRepo = _repository.GetStopForTrip(tripId, id);
            if(stopForTripFromRepo == null)
            {
                return NotFound();
            }
            var stopForTrip = _mapper.Map<StopModel>(stopForTripFromRepo);
            return Ok(stopForTrip);


        }

        [HttpPost]
        public async Task<IActionResult> CreatStopForTrip(int tripId, [FromBody] StopForCreationModel stop)
        {
            if(stop == null)
            {
                return BadRequest();
            }
            if(!_repository.TripExists(tripId))
            {
                return NotFound();
            }
            var stopEntity = _mapper.Map<Stop>(stop);

            stopEntity.TripId = tripId;
            _repository.Add(stopEntity);

            if (!await _repository.SaveChangesAsync())
            {
                throw new Exception($"Creating a stop for trip {tripId} failed on save");
            }

            var stopToReturn = _mapper.Map<StopModel>(stopEntity);

            return CreatedAtRoute("GetStopForTrip",
                        new { tripId = tripId, id = stopToReturn.Id },
                        stopToReturn);
        }
    }
}
