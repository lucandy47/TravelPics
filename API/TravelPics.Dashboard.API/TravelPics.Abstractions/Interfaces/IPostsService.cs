﻿using TravelPics.Abstractions.DTOs.Posts;

namespace TravelPics.Abstractions.Interfaces
{
    public interface IPostsService
    {
        Task SavePost(PostDTO postDTO, CancellationToken cancellationToken);

        Task<IEnumerable<PostDTO>> GetUserPosts(int userId);

        Task<IEnumerable<PostDTO>> GetLatestPosts();


    }
}
