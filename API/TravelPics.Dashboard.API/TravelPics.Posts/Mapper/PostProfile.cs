using AutoMapper;
using TravelPics.Domains.Entities;
using TravelPics.Posts.Abstraction.DTO;

namespace TravelPics.Posts.Mapper
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostDTO>();
        }
    }
}
