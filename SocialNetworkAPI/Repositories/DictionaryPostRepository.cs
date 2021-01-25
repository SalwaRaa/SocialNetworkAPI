using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Repositories
{
    public class DictionaryPostRepository : IPostRepository
    {
        public Dictionary<Guid, Post> _post = new Dictionary<Guid, Post>();
    }
}
