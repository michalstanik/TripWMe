using AutoMapper;
using TripWMe.App.Models;
using TripWMe.Domain;

namespace TripWMe.App.Profiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Trip, TripModel>().ReverseMap();
        }   
    }
}
