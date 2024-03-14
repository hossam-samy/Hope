using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfShelters
{
    public class GetAllPostsOfSheltersQuery : IRequest<Response>
    {
        public int? cursor { get; set; }
        public string UserId { get; set; }
       
    }
}
