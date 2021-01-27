using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Services
{
    public interface IPostService
    {
        void ApplyPatch(Post post, Dictionary<string, object> patches);
        void ApplyPatch<T>(T original, Dictionary<string, object> patches);
        void LikePost(Post post, User user);
        void UnLikePost(Post post, User user);
    }
}
