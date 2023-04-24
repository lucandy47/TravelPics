using AutoMapper;
using Polly;
using System.Reflection.Metadata;
using System.Threading;
using TravelPics.Documents.Abstraction;
using TravelPics.Documents.Abstraction.DTO;
using TravelPics.Domains.Entities;
using TravelPics.Posts.Abstraction;
using TravelPics.Posts.Abstraction.DTO;
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
                var photo = await _documentsService.ComputeDocument(photoDTO, documentBlobContainerDTO, "test", cancellationToken);
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