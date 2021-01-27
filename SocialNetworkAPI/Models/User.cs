using SocialNetworkAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Models
{
    public class User : UserDto
    {
        /// <summary>
        /// A unique guid genereated identifier for a user
        /// </summary>
        /// <example>7b87d7bb-b0cd-4f1b-812d-c80f9d400sdy</example>
        public Guid Id { get; set; }

        // empty const for the test model in userRepo
        public User()
        {

        }

        //have to create a const since the parent{User} cant become one of the kids{UserDto}
        public User(UserDto userDto)
        {
            Id = Guid.NewGuid();
            UserName = userDto.UserName;
            EmailAdress = userDto.EmailAdress;
        }
    }
}
