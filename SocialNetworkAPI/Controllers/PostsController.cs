using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.Dtos;
using SocialNetworkAPI.Models;
using SocialNetworkAPI.Repositories;
using SocialNetworkAPI.Services;
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
        private readonly IUserRepository _userRepository;
        private readonly IPostService _postService;


        [HttpPost]
        public ActionResult CreatePost(PostDto postDto)
        {
            var user = _userRepository.GetUserById(postDto.CreatedBy);
            if (user is null)
            {
                return NotFound($"The userId: '{postDto.CreatedBy}' could not be found in the system");
            }
            var post = _postRepository.Add(postDto, user);
            return CreatedAtAction(nameof(GetPost), new { id = user.Id }, user);
        }


        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }


        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<Post> GetPost(Guid id)
        {
            var post = _postRepository.GetPostById(id);
            if (post is null)
            {
                return NotFound($"The postId: '{id}' you are looking for could not be found in the system");
            }
            return post;
        }


        [HttpPatch]
        [Route("{id:guid}/update/{createdby:guid}")]
        public ActionResult UpdatePost(Guid id, Dictionary<string, object> patches, Guid createdBy)
        {
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"The userId: '{createdBy}' could not be found in the system");
            }

            var post = _postRepository.GetPostById(id);
            if (post is null)
            {
                return NotFound($"The postId: '{id}' could not be found in the system");
            }

            if (post.CreatedBy != user)
            {
                return BadRequest($"User with ud {createdBy} has no authorazation for the request");
            }
            _postService.ApplyPatch(post, patches);
            return NoContent();
        }


        [HttpPatch]
        [Route("{id:guid}/delete/{createdby:guid}")]
        public ActionResult DeletePost(Guid id, Guid createdBy)
        {
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"The userId: '{createdBy}' could not be found in the system");
            }

            var post = _postRepository.GetPostById(id);
            if (post is null)
            {
                return NotFound($"The postId: '{id}' could not be found in the system");
            }

            if (post.CreatedBy != user)
            {
                return BadRequest($"User with userId: '{createdBy}' has no authorazation for the request");
            }
            _postRepository.DeletePost(post);
            return NoContent();
        }


        [HttpPut]
        [Route("{id:guid}/like{createdby:guid}")]
        public ActionResult LikePost(Guid id, Guid createdBy)
        {
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"The userId: '{createdBy}' could not be found in the system");
            }

            var post = _postRepository.GetPostById(id);
            if (post is null)
            {
                return NotFound($"The postId: '{id}' could not be found in the system");
            }

            var nonUniqueLike = post.UserLikes.Contains(user);
            if (nonUniqueLike)
            {
                return BadRequest($"User with userId: '{createdBy}' has no authorazation for the request");
            }
            _postService.LikePost(post, user);
            return NoContent();
        }


        [HttpPut]
        [Route("{id:guid}/unlike{createdby:guid}")]
        public ActionResult UnLikePost(Guid id, Guid createdBy)
        {
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"The userId: '{createdBy}' could not be found in the system");
            }

            var post = _postRepository.GetPostById(id);
            if (post is null)
            {
                return NotFound($"The postId: '{id}' could not be found in the system");
            }

            var nonUniqueUnLike = post.UserLikes.Contains(user);
            if (nonUniqueUnLike)
            {
                return BadRequest($"User with userId: '{createdBy}' has no authorazation for the request");
            }
            _postService.UnLikePost(post, user);
            return NoContent();
        }
    } 
}
