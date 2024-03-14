using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Commands.UpdateComment
{
    public class UpdateCommentCommand:IRequest<Response>
    {
        
        public string? UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
