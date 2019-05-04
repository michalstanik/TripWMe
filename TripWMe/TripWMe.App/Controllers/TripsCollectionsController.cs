using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.CoreHelpers.Binders;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.Trips;
using TripWMe.Models.Trips;

namespace TripWMe.App.Controllers
{
    [Route("api/tripscollections/")]
    [ApiController]
    public class TripsCollectionsController : Controller
    {
        private readonly ITripRepository _repository;
        private readonly IMapper _mapper;

        public TripsCollectionsController(ITripRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTripsCollection (IEnumerable<TripForCreationModel> tripCollection)
        {
            if (tripCollection == null)
            {
                return BadRequest();
            }
            var tripEntities = _mapper.Map<ICollection<Trip>>(tripCollection);

            foreach (var trip in tripEntities)
            {
                _repository.Add(trip);
            }

            if (!await _repository.SaveChangesAsync())
            {
                throw new Exception("Creating an trip collection failed on save");
            }

            var tripCollectionToReturn = _mapper.Map<IEnumerable<TripModel>>(tripEntities);
            var idAsString = string.Join(",", tripCollectionToReturn.Select(t => t.Id));

            //TODO: Uncoment when null Model in ModelBindingContext will be fixed

            //return CreatedAtRoute("GetTripCollection",
            //    new { ids = idAsString },
            //    tripCollectionToReturn);
            return Ok();
        }

        //TODO: Fix null Model in ModelBindingContext
        [HttpGet("{(ids)}", Name = "GetTripCollection")]
        public IActionResult GetTripCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> ids)
        {
            if(ids == null)
            {
                return BadRequest();
            }

            var tripEntities = _repository.GetTrips(ids);

            if( ids.Count() != tripEntities.Count())
            {
                return NotFound();
            }

            var tripToReturn = _mapper.Map<IEnumerable<TripModel>>(tripEntities);
            return Ok(tripToReturn);
        }
     
    }
}
