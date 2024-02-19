using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class PostDto
    {
        public int? Age { get; set; }
        public string? Condition { get; set; }
        public string? Name { get; set; }
        public string? Gendre { get; set; }
        public string? Type { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
