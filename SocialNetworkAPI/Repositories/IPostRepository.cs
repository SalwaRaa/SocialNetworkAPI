using SocialNetworkAPI.Dtos;
using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Repositories
{
    public interface IPostRepository
    {
        Post Add(PostDto postDto, User user);
        void DeletePost(Post post);
        Post GetPostById(Guid id);
        IEnumerable<Post> GetPosts();
        void UpdatePost(Post post);
    }
}
