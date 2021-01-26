using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SocialNetworkAPI.Repositories
{
    public class DictionaryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>();
        
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
