using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public abstract class Post
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }=false;  
        public bool IsFound { get; set; }=false; 

    }
}
