using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class PostOfLostPeople:Post
    {
        
        public int Age { get; set; }
        public string Condition { get; set; }
        public string Name { get; set; }
        public string Gendre { get; set; }
        
        public string UserId { get; set; }
        public virtual User user { get; set; }

    }
}
