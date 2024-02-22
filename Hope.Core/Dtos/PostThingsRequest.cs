using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class PostThingsRequest
    {
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }

    }
}
