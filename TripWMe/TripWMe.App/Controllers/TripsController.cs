using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace TripWMe.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public TripsController(IMapper mapper, LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }



    }
}
