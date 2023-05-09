using TravelPics.Abstractions.DTOs.Likes;
using TravelPics.Abstractions.DTOs.Posts;
using TravelPics.Domains.Entities;

namespace TravelPics.Abstractions.Interfaces
{
    public interface IPostsService
    {
        Task SavePost(PostDTO postDTO, CancellationToken cancellationToken);

        Task<IEnumerable<PostDTO>> GetUserPosts(int userId);

        Task<IEnumerable<PostDTO>> GetLatestPosts();

        Task LikePost(LikeModel like);
        Task DislikePost(LikeModel like);

        Task<PostDTO> GetPostById(int postId);

    }
}
