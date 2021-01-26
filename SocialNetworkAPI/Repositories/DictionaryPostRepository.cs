using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Repositories
{
    //DictionaryPostRepository has been implemented as an interface of IPostRepository
    public class DictionaryPostRepository : IPostRepository
    {
        public readonly Dictionary<Guid, Post> _post = new Dictionary<Guid, Post>();
    }
}
