using Hope.Core.Common;
using MediatR;
using System.Runtime.Serialization;

namespace Hope.Core.Features.CommentOperation.Commands.DeleteComment
{
    public class DeleteCommentCommand:IRequest<Response>
    {
        public string? UserId { get; set; }
        public int CommentId { get; set; }
    }
}
