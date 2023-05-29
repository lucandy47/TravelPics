using TravelPics.Domains.Entities;

namespace TravelPics.Posts.Repository
{
    public interface IPostsRepository
    {
        Task SavePost(Post post);
        Task<IEnumerable<Post>> GetUserPosts(int userId);
        Task<IEnumerable<Post>> GetLatestPosts();
        Task LikePost(int userId, int postId);
        Task DislikePost(int userId, int postId);
        Task<Post> GetPostById(int postId);
        Task<IEnumerable<Post>> GetAllPosts();
    }
}
