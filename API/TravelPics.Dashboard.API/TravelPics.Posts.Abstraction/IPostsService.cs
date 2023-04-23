using TravelPics.Posts.Abstraction.DTO;

namespace TravelPics.Posts.Abstraction
{
    public interface IPostsService
    {
        Task SavePost(PostDTO postDTO);
    }
}