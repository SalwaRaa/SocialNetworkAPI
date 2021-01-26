using SocialNetworkAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Models
{
    public class User : UserDto
    {
        public Guid Id { get; set; }

        // empty const  for the test model in userRepo
        public User()
        {

        }
        public User(UserDto userDto)
        {
            Id = Guid.NewGuid();
            UserName = userDto.UserName;
            EmailAdress = userDto.EmailAdress;
        }
    }
}
