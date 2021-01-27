using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkAPI.Dtos;

namespace SocialNetworkAPI.Repositories
{
    public class DictionaryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>();

        // created a constructor to hardcode add new users (test models)
        public DictionaryUserRepository()
        {
            // new user object
            var user = new User()
            {
                Id = new Guid("543e1eb1-61a9-44c0-a306-ea1f5d5e7494"),
                UserName = "Salwa",
                EmailAdress = "Hej@live.se"
            };
            var user2 = new User()
            {
                Id = new Guid("8927bd6a-0f41-4068-bfc8-5baaafef5587"),
                UserName = "Bez",
                EmailAdress = "Hejdå@live.se"
            };
            // {user} is the complete user object
            _users.Add(user.Id, user);
            _users.Add(user2.Id, user2);
        }

        //the Guid will be responsible to generate the id. The repo will return a complete user object even when we only pass in userDto
        public User Add(UserDto userDto)
        {
            var user = new User(userDto);
            _users.Add(user.Id, user);
            return user;
        }

        public IEnumerable<User> GetUsers()
       {
            //return all the values as they are from our dictionary
            return _users.Values;
       }

        public User GetUserById(Guid id)
        {
            //establish a behaviour that checks if the id exists and if not it will return null
            if (_users.ContainsKey(id))
            {
                return _users[id];
            }
            return null;
        }

        public bool NonUniqueUserName(UserDto userDto)
        {
            return _users.Any(e => e.Value.UserName == userDto.UserName);
        }
    }
}
