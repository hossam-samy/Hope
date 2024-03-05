using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class CommentRequest
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
