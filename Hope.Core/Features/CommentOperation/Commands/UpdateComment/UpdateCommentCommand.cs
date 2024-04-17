using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.CommentOperation.Commands.UpdateComment
{
    public class UpdateCommentCommand:IRequest<Response>
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
