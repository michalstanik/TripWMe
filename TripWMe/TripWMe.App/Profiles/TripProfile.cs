using AutoMapper;

namespace TripWMe.App.Profiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Domain.Trips.Trip, Models.Trips.TripModel>().ReverseMap();
            CreateMap<Domain.Trips.Trip, Models.Trips.TripWithTripManager>();
            CreateMap<Domain.Trips.Trip, Models.Trips.TripWithStats>();

            CreateMap<Models.Trips.TripForCreationModel, Domain.Trips.Trip>();
            CreateMap<Models.Stops.StopForCreationModel, Domain.Stops.Stop>();
            CreateMap<Models.Stops.StopForUpdateModel, Domain.Stops.Stop>();
        }   
    }
}
