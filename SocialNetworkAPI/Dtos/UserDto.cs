using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Dtos
{
    /// <summary>
    /// Used when creating a new user. Some properties have been simplified such as CreatedBy that only accepts an Id instead of an User object.
    /// </summary>
    public class UserDto
    {
        private const string _stringMessage= "{0} must only contain letters and numbers with no whitespace";
        private const string _stringMessageEmail = "{0} must only contain letters, numbers and special characters with no whitespace";
        
        /// <summary>
        /// A human readable name
        /// Can only contain alphanumeric characters with no whitespace
        /// </summary>
        /// <example>helloWorld2</example>
        [Required]
        [RegularExpression("^([A-Za-z0-9])*$", ErrorMessage = _stringMessage)]
        public string UserName { get; set; }

        /// <summary>
        /// The users email address information.
        /// Must be a valid e-mail address, but this is not thoroughly checked.
        /// To make sure the e-mail address is correct, we might want to use an outside service for e-mail address confirmation.
        /// <example>helloWorld2</example>
        [Required]
        [RegularExpression(@"^([A-Za-z0-9\s])$", ErrorMessage = _stringMessageEmail)]
        public string EmailAdress { get; set; }
    }
}
