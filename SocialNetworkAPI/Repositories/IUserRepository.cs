using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUserById(Guid id);
    }
}
