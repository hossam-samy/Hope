using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToComment
{
    public class AddCommentToCommentCommand:IRequest<Response>
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
