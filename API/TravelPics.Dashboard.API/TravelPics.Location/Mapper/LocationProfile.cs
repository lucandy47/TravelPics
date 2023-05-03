using AutoMapper;
using TravelPics.Abstractions.DTOs.Locations;
using TravelPics.Domains.Entities;

namespace TravelPics.Locations.Mapper
{
    public class LocationProfile: Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationDTO, Location>();
            CreateMap<Location, LocationDTO>();
        }
    }
}
