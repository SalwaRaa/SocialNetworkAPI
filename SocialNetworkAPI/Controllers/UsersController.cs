using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository _userRepository;

        //creates an instance of the controller (this happenes in every request)
        //when it wants a IuserRepo give it a UserRepo
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public void Example()
        {
            System.Console.WriteLine();
        }
    }
}
