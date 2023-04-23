using AutoMapper;
using TravelPics.Documents.Abstraction;
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

        public async Task SavePost(PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
        }
    }
}