using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetPostByPostId
{
    public class GetPostByPostIdQuery:IRequest<Response>
    {
        public int PostId { get; set; }
        public bool IsPeople { get; set; }

    }
}
