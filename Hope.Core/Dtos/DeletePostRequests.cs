using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class DeletePostRequests
    {
       public string UserId { get; set; }
       public int PostId { get; set; }
       public bool IsPeople { get; set; }
    }
    public class DeleteCommentRequests
    {
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public bool IsPeople { get; set; }
    }

}
