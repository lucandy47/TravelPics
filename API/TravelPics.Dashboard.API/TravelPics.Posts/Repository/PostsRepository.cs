using Microsoft.EntityFrameworkCore;
using Polly;
using System.Reflection.Metadata;
using TravelPics.Domains.DataAccess;
using TravelPics.Domains.Entities;

namespace TravelPics.Posts.Repository
{
    public class PostsRepository: IPostsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> GetUserPosts(int userId)
        {
            var posts = await _dbContext.Posts
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.Photos)
                .Where(p => p.CreatedById == userId && !p.IsDeleted)
                .OrderByDescending(p => p.PublishedOn)
                .ToListAsync();

            return posts;
        }

        public async Task SavePost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to save post to database.", ex);
            }
        }
    }
}
