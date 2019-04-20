using AutoMapper;
using TripWMe.Domain.Trips;
using TripWMe.Models;

namespace TripWMe.App.Profiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Trip, TripModel>().ReverseMap();
            CreateMap<Trip, TripWithStats>();
        }   
    }
}
