using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToPost
{
    public class AddCommentToPostCommand:IRequest<Response>
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
