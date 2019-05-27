using AutoMapper;

namespace TripWMe.App.Profiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Domain.Trips.Trip, Models.Trips.TripModel>().ReverseMap();
            CreateMap<Domain.Trips.Trip, Models.Trips.TripWithStopsModel>();
            CreateMap<Domain.Trips.Trip, Models.Trips.TripWithStopsAndUsersModel>();
            CreateMap<Domain.Trips.Trip, Models.Trips.TripWithTripManager>();
            CreateMap<Domain.Trips.Trip, Models.Trips.TripWithStatsModel>();

            CreateMap<Domain.GeoEntities.Country, Models.GeoEntities.CountryModelWithAssesments>();

            CreateMap<Models.Trips.TripForCreationModel, Domain.Trips.Trip>();
            CreateMap<Models.Trips.TripWithTripManagerForCreation, Domain.Trips.Trip>();
            CreateMap<Models.Trips.TripWithStopsForCreationModel, Domain.Trips.Trip>();

            CreateMap<Models.Stops.StopForCreationModel, Domain.Stops.Stop>();
            CreateMap<Models.Stops.StopForUpdateModel, Domain.Stops.Stop>();

            CreateMap<Models.User.TUserForCreationModel, Domain.User.TUser>();
        }   
    }
}
