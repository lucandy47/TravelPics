using AutoMapper;
using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Likes;
using TravelPics.Abstractions.DTOs.Posts;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Domains.Entities;
using TravelPics.Posts.Repository;

namespace TravelPics.Posts
{
    public class PostsService: IPostsService
    {
        private readonly IDocumentsService _documentsService;
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;

        public PostsService(IDocumentsService documentsService, IPostsRepository postsRepository,IMapper mapper)
        {
            _documentsService = documentsService;
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task DislikePost(LikeModel like)
        {
            await _postsRepository.DislikePost(like.UserId, like.PostId);
        }

        public async Task<IEnumerable<PostDTO>> GetLatestPosts()
        {
            var posts = await _postsRepository.GetLatestPosts();

            var postsDTO = _mapper.Map<List<PostDTO>>(posts);

            if (postsDTO == null) throw new Exception($"Could not get latest posts.");

            return postsDTO;
        }

        public async Task<PostDTO> GetPostById(int postId)
        {
            var post = await _postsRepository.GetPostById(postId);

            var postDTO = _mapper.Map<PostDTO>(post);

            if (postDTO == null) throw new Exception($"Could not map the post.");

            return postDTO;
        }

        public async Task<IEnumerable<PostDTO>> GetUserPosts(int userId)
        {
            var posts = await _postsRepository.GetUserPosts(userId);

            var postsDTO = _mapper.Map<List<PostDTO>>(posts);

            if (postsDTO == null) throw new Exception($"Could not get the posts for user: {userId}.");

            return postsDTO;
        }

        public async Task LikePost(LikeModel like)
        {
            await _postsRepository.LikePost(like.UserId, like.PostId);
        }

        public async Task SavePost(PostDTO postDTO, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(postDTO);

            var documentBlobContainerDTO = new DocumentBlobContainerDTO()
            {
                Id = 3
            };
            if (post == null || !post.Photos.Any()) throw new Exception($"Could not get the post.");

            post.Photos.Clear();

            foreach (var photoDTO in postDTO.Photos)
            {
                var photo = await _documentsService.ComputeDocument(photoDTO, documentBlobContainerDTO, "test", false, cancellationToken);
                post.Photos.Add(photo);
            }

            await _postsRepository.SavePost(post);

            try
            {
                await _documentsService.UploadPhotos(post.Photos.ToList(), documentBlobContainerDTO, cancellationToken);
            }
            catch(Exception ex)
            {
                throw new Exception($"Unable to upload photos to cloud.", ex);
            }
        }
    }
}