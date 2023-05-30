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

        public async Task DislikePost(int userId, int postId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (user == null || post == null)
            {
                throw new Exception($"User with {userId} could not like post with id {postId}");
            }

            var existingLike = await _dbContext.Likes.FirstOrDefaultAsync(l => l.User.Id == userId && l.Post.Id == postId && !l.IsDeleted);

            if (existingLike == null)
            {
                throw new Exception($"No 'like' relation between user: {userId} and post with id {postId} was found");
            }

            existingLike.IsDeleted = true;
            existingLike.DislikedOn = DateTimeOffset.Now;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update dislike in database.", ex);
            }
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var posts = await _dbContext.Posts
                .Include(p => p.User)
                    .ThenInclude(u => u.ProfileImage)
                .Include(p => p.Location)
                .Include(p => p.Photos)
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<Post>> GetLatestPosts()
        {
            var currentDay = DateTimeOffset.Now.Date;

            var posts = await _dbContext.Posts
                .Include(p => p.User)
                    .ThenInclude(u => u.ProfileImage)
                .Include(p => p.Location)
                .Include(p => p.Photos)
                .Include(p => p.Likes.Where(l => !l.IsDeleted))
                    .ThenInclude(l => l.User)
                .Where(p => p.PublishedOn >= currentDay.AddDays(-7))
                .OrderByDescending(p => p.PublishedOn)
                .ToListAsync();

            return posts;

        }

        public async Task<IEnumerable<Post>> GetLocationPosts(string locationName)
        {
            var posts = await _dbContext.Posts
                .Include(p => p.User)
                    .ThenInclude(u => u.ProfileImage)
                .Include(p => p.Location)
                .Include(p => p.Photos)
                .Include(p => p.Likes.Where(l => !l.IsDeleted))
                    .ThenInclude(l => l.User)
                .Where(p => p.Location.Address == locationName)
                .OrderByDescending(p => p.PublishedOn)
                .ToListAsync();

            return posts;
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _dbContext.Posts
                .Include(p => p.User)
                    .ThenInclude(u => u.ProfileImage)
                .Include(p => p.Location)
                .Include(p => p.Photos)
                .Include(p => p.Likes.Where(l => !l.IsDeleted))
                    .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null) throw new Exception($"No post found with id: {postId}");
            return post;
        }

        public async Task<IEnumerable<Post>> GetUserPosts(int userId)
        {
            var posts = await _dbContext.Posts
                .Include(p => p.User)
                    .ThenInclude(u => u.ProfileImage)
                .Include(p => p.Location)
                .Include(p => p.Photos)
                .Include(p => p.Likes.Where(l => !l.IsDeleted))
                    .ThenInclude(l => l.User)
                .Where(p => p.CreatedById == userId && !p.IsDeleted)
                .OrderByDescending(p => p.PublishedOn)
                .ToListAsync();

            return posts;
        }

        public async Task LikePost(int userId, int postId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (user == null || post == null)
            {
                throw new Exception($"User with {userId} could not like post with id {postId}");
            }

            var existingLike = await _dbContext.Likes.FirstOrDefaultAsync(l => l.User.Id == userId && l.Post.Id == postId && l.IsDeleted);

            if(existingLike == null)
            {
                var like = new Like()
                {
                    User = user,
                    Post = post,
                    LikedOn = DateTimeOffset.Now
                };

                await _dbContext.Likes.AddAsync(like);
            }
            else
            {
                existingLike.LikedOn = DateTimeOffset.Now;
                existingLike.IsDeleted = false;
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to save new like to database.", ex);
            }
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
