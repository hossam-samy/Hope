using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class PostOfLostPeople:Post
    {
        //
        public int? Age { get; set; }
        public string Condition { get; set; }
        //
        public string? Name { get; set; }
        public string Gendre { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

         public int ? CommentCount=>Comments?.Count;

        public bool IsPeople =>true;
        public virtual User User { get; set; } 

        public virtual List<User> HiddenPeoples { get; set; }

        public virtual List<User> PinnedPeoples { get; set; }

        public virtual List<Comment> Comments { get; set; }

       
    }
}
