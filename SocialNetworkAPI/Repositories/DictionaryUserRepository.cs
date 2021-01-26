using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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
                UserName = "Salwa"
            };
            var user2 = new User()
            {
                    Id = new Guid("8927bd6a-0f41-4068-bfc8-5baaafef5587"),
                    UserName = "Bez"
            };
            // "user" is the complete user object
            _users.Add(user.Id, user);
            _users.Add(user2.Id, user2);
        }
        
        
        public IEnumerable<User> GetUsers()
       {
            //return all the values as they are from our dictionary
            return _users.Values;
       }

        public User GetUserById(Guid id)
        {
            if (_users.ContainsKey(id))
            {
                return _users[id];
            }
            return null;
        }
    }
}
