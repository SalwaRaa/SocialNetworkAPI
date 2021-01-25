using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
        {
            private readonly IPostRepository _postRepository;

            //creates an instance of the controller (this happenes in every request)
            //when it wants a IpostRepo give it a PostRepo
            public PostsController(IPostRepository postRepository)
            {
                _postRepository = postRepository;
            }
            [HttpGet]
            public void Example()
            {
                System.Console.WriteLine();
            }
        }
    }

