using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPosts
{
    public class GetAllPostsQuery:IRequest<Response>
    {
        public int PageNumber { get; set; }
        public string UserId { get; set; }
    }
}
    