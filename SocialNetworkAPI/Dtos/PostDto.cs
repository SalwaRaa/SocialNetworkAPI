using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkAPI.Dtos
{
    public class PostDto
    {
        public string PostContent { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
