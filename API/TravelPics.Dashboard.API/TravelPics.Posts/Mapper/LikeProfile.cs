using AutoMapper;
using TravelPics.Abstractions.DTOs.Likes;
using TravelPics.Domains.Entities;

namespace TravelPics.Posts.Mapper
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Post.Id));
        }
    }
}
