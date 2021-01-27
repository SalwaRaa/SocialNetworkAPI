using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using SocialNetworkAPI.Dtos;

namespace SocialNetworkAPI.Repositories
{
    public interface IUserRepository
    {
        User Add(UserDto userDto);
        IEnumerable<User> GetUsers();
        User GetUserById(Guid id);
        bool NonUniqueUserName(UserDto userDto);
    }
}
