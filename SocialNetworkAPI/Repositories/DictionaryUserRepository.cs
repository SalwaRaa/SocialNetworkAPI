using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Repositories
{
    public class DictionaryUserRepository : IUserRepository
    {
        private Dictionary<Guid, User> _user = new Dictionary<Guid, User>();
    }
}
