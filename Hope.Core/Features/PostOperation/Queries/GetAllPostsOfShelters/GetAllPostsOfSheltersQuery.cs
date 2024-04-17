using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfShelters
{
    public class GetAllPostsOfSheltersQuery : IRequest<Response>
    {
        public int PageNumber { get; set; }
       
    }
}
