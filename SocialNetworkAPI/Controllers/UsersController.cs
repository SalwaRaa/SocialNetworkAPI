using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.Dtos;
using SocialNetworkAPI.Models;
using SocialNetworkAPI.Repositories;
using System;
using System.Collections.Generic;

namespace SocialNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
   
    public class UsersController : ControllerBase
    {
        //creates an instance of the controller(this happenes in every request)
        //when it wants a IuserRepo give it a UserRepo
        //cons
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        //passes in UserDto so I dont pass in the Id
        public ActionResult CreateUser(UserDto userDto)
        {
            if (userDto is null)
            {
                return BadRequest("Missing User");
            }
            var nonUnique = _userRepository.NonUniqueUserName(userDto);
            if (nonUnique)
            {
                return BadRequest("The chosen user name already exists. Please re-enter a new user name");
            }
            var user = _userRepository.Add(userDto);
            //the user was created. To get the user use the method {GetUser} with the ID that was created. {user] is what was created
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }


        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }


        [HttpGet]
        [Route("{id:guid}")]
        //ActionResult should always be used except for the Get collections that only returns OK. 
        public ActionResult<User> GetUser(Guid id)
        {
            var user = _userRepository.GetUserById(id);
           if (user is null)
            {
               //return 404
                return NotFound($"The userId: '{id}' you are looking for could not be found in the system");
            }
            return user;  
        }
    }
}
