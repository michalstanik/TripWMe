using AutoMapper;
using TripWMe.Domain;
using TripWMe.Models;

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
