using SocialNetworkAPI.Dtos;
using SocialNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkAPI.Repositories
{
    public class DictionaryPostRepository : IPostRepository
    {
        public readonly Dictionary<Guid, Post> _posts = new Dictionary<Guid, Post>();

        public DictionaryPostRepository(IUserRepository userRepository)
        {
            var user = userRepository.GetUsers();
          
            var post = new Post()
            {
                Id = new Guid("ac4c46c3-12e7-45e2-bc55-da775cc03fdb"),
                PostContent = "Cant wait until summer is here",
                //used linq since I used Ienumerable which is not a list, that doesnt use index
                CreatedBy = user.First()
            };
            var post2 = new Post()
            {
                Id = new Guid("7b87d7bb-b0cd-4f1b-812d-c80f9d400ad5"),
                PostContent = "Cant wait until graduation is finaly here",
                CreatedBy = user.Last()
            };
            
            _posts.Add(post.Id, post);
            _posts.Add(post2.Id, post2);
        }

        //passes in user so only an existing user can create a post and connect it with the user
        public Post Add(PostDto postDto, User user)
        {
            //generates a guid id for the post when created
            var id = Guid.NewGuid();
            var post = new Post(id, postDto, user);
            _posts.Add(post.Id, post);
            return post;
        }

        public Post GetPostById(Guid id)
        {
            if (_posts.ContainsKey(id))
            {
                return _posts[id];
            }
            return null;
        }

        public IEnumerable<Post> GetPosts()
        {
            return _posts.Values;
        }

        public void DeletePost(Post post)
        {
            _posts.Remove(post.Id);
        }

        public void UpdatePost(Post post)
        {
            //To edit the post we first remove it and add a new one with the updated content. {post} is the complete post object
            _posts.Remove(post.Id);
            _posts.Add(post.Id, post);
        }
    }
}
