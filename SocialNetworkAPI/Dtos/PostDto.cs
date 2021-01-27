using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Dtos
{
    /// <summary>
    /// Used when creating a new post. Some properties have been simplified such as CreatedBy that only accepts an Id instead of an User object.
    /// </summary>
    public class PostDto
    {
        private const string _stringMessage = "{0} must be between {2} and {1} characters long";

        /// <summary>
        /// A post posted by a existing user into the social netowrk app. The post contains text.
        /// An existing user is required when creating and updating a post. Can only be created by the same post creator.
        /// </summary>
        /// <example>I love candy and gaming!</example>
        [Required]
        [StringLength(400, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string PostContent { get; set; }

        /// <summary>
        /// Guid generated id that is able to create a post. Must be an existing user
        /// </summary>
        /// <example>7b87d7bb-b0cd-4f1b-812d-c80f9d400dfrt</example>
        public Guid CreatedBy { get; set; }
    }
}
