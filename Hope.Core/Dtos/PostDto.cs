using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    
    public  class PostDto
    {
        public List<PostPeopleResponse>?  PeopleResponses { get; set; }
        public List<PostThingResponse>?   ThingResponses { get; set; }

    }
}
