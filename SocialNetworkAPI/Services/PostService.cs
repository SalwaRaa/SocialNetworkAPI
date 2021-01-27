using SocialNetworkAPI.Models;
using SocialNetworkAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Services
{
    //Created separete service class since repo should only contain data storage
    //since the methods in services class are a combination of the repos and some logic
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void ApplyPatch(Post post, Dictionary<string, object> patches)
        {
            ApplyPatch<Post>(post, patches);
            post.IsUpdated = true;
            post.LatestUpdate = DateTime.Now;
        }

        public void ApplyPatch<T>(T original, Dictionary<string, object> patches)
        {
            var properties = original.GetType().GetProperties();
            foreach (var patch in patches)
            {
                foreach (var prop in properties)
                {
                    if (string.Equals(patch.Key, prop.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        prop.SetValue(original, patch.Value);
                    }
                }
            }
        }

        public void LikePost(Post post, User user)
        {   //adds a like in the post that is will be saved in the <List>prop and updates the post with the like containing it
            post.UserLikes.Add(user);
            _postRepository.UpdatePost(post);
        }

        public void UnLikePost(Post post, User user)
        {
            post.UserLikes.Remove(user);
            _postRepository.UpdatePost(post);
        }
    }
}