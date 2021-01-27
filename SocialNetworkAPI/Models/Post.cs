using SocialNetworkAPI.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// A unique guid genereated identifier for each post
        /// </summary>
        /// <example>7b87d7bb-b0cd-4f1b-812d-c80f9d400sdy</example>
        public Guid Id { get; set; }

        public string PostContent { get; set; }

        public User CreatedBy { get; set; }

        /// <summary>
        /// Checks if a post has been updated. If it has this property will always be true.
        /// </summary>
        /// <example>false</example>
        public bool IsUpdated { get; set; }

        //property that demonstrate the time a post was updated

        /// <summary>
        /// When a post content is updated, date and time of the change will be set 
        /// </summary>
        public DateTime LatestUpdate { get; set; }

        //property that lists all liked and unliked posts by users

        /// <summary>
        /// Stores users who likes a post. A user can only like a post once. A user can unlike the post.
        /// </summary>
        public List<User> UserLikes { get; set; } = new List<User>();

        public Post()
        {
        }

       
    }
}
