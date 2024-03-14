using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPosts
{
    public class GetAllPostsQuery:IRequest<Response>
    {
        public int? Peoplecursor { get; set; }
        public int? thingcursor { get; set; }
        public string UserId { get; set; }
    }
}
