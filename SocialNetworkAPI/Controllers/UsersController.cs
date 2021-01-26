using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.Models;
using SocialNetworkAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("{id:guid}")]
        //ActionResult should always be used except for the Get collections that only returns OK. 
        public ActionResult<User> GetUserById(Guid id)
        {
            var user = _userRepository.GetUserById(id);
           if (user is null)
            {
                return NotFound($"The userId: '{id}' you are looking for could not be found in the system");
            }
            return user;  
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }


        

       
    }
}
