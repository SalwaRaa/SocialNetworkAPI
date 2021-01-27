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
    /// <summary>
    /// Controller with working with posts
    /// </summary>
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostService _postService;

        public PostsController(IPostRepository postRepository, IUserRepository userRepository, IPostService postService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _postService = postService;
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        ///<param name="postDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all existing posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        /// <summary>
        /// Gets a post with a specific id
        /// </summary>
        /// <param name="id">The id of an existing specific post</param>
        /// <response code="200">Returns the post with the given id</response>
        /// <response code="404">If no post with the given id was found</response>
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

        /// <summary>
        /// Selectively updates all post properties on an existing post. Can only be done by the user who created the post
        /// </summary>
        ///<param name="createdBy">The id of the user that wants to update the given post</param>
        ///<param name="id">The id of an existing specific post</param>
        /// <param name="patches">The new post content</param>
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
                return BadRequest($"User with id '{createdBy}' has no authorazation for the request");
            }
            _postService.ApplyPatch(post, patches);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing post. Can only be done by the user who created the post
        /// </summary>
        /// <param name="id">The id of a specific post that will be deleted</param>
        /// <param name="createdBy">The id of the user that deletes the post</param>
        /// <returns></returns>
        [HttpDelete]
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

        /// <summary>
        /// Adds a like to an existing post. Can only be done by a user that haven't liked the post.
        /// </summary>
        /// <param name="id">The id of a specific post that will be liked</param>
        /// <param name="createdBy">The id of the user that likes the post</param>
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

        /// <summary>
        /// Removes a like from an existing post. Can only be done by a user who previously liked the post.
        /// </summary>
        /// <param name="id">The id of a specific post that will be unliked</param>
        /// <param name="createdBy">The id of the user that unlike the post</param>
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
