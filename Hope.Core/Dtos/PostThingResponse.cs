using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class PostThingResponse
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public string UserName { get; set; }
        public string City { get; set; }
        public string UserImage { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
