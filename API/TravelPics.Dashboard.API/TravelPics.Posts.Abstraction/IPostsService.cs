using TravelPics.Domains.Entities;
using TravelPics.Posts.Abstraction.DTO;

namespace TravelPics.Posts.Abstraction
{
    public interface IPostsService
    {
        Task SavePost(PostDTO postDTO, CancellationToken cancellationToken);

        Task<IEnumerable<PostDTO>> GetUserPosts(int userId);

    }
}