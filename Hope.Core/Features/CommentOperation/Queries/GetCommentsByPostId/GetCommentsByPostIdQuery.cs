using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.CommentOperation.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQuery:IRequest<Response>
    {
          public bool IsPeople { get; set; }

          public int PostId { get; set; }   
    }
}
