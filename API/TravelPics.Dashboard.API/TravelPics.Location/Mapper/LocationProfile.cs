using AutoMapper;
using TravelPics.Domains.Entities;
using TravelPics.Locations.Abstraction.DTO;

namespace TravelPics.Locations.Mapper
{
    public class LocationProfile: Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationDTO, Location>();
        }
    }
}
