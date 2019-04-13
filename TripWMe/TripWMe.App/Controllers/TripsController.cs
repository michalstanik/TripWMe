using AutoMapper;
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
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public async Task<ActionResult<List<TripModel>>> GetAllProjects(bool includeStops = false, bool includeUsers = false)
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


    }
}
