using SocialNetworkAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Models
{
    public class Post 
    { 
        public Post(Guid id, PostDto postDto, User user)
        {
            Id = id;
            PostContent = postDto.PostContent;
            CreatedBy = user;
        }

        public Guid Id { get; set; }

        public string PostContent { get; set; }

        public User CreatedBy { get; set; }

        //property that demonstrate if a post has been updated
        public bool IsUpdated { get; set; }

        //property that demonstrate the time a post was updated
        public DateTime LatestUpdate { get; set; }

        //property that lists all liked and unliked posts by users
        public List<User> UserLikes { get; set; } = new List<User>();

        public Post()
        {
        }

       
    }
}
