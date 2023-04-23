using TravelPics.Domains.Entities;

namespace TravelPics.Posts.Repository
{
    public interface IPostsRepository
    {
        Task SavePost(Post post);

    }
}
