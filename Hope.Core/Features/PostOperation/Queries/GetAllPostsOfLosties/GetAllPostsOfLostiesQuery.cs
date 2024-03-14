using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfLosties
{
    public class GetAllPostsOfLostiesQuery:IRequest<Response>
    {
        public int? cursor { get; set; }
        public string UserId { get; set; }
       
    }
}
