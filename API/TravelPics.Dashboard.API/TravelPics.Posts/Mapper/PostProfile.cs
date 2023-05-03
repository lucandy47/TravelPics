using AutoMapper;
using TravelPics.Abstractions.DTOs.Posts;
using TravelPics.Domains.Entities;

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
