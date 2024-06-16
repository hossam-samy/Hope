using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfThings
{
    public class GetAllPostsOfThingsQuery:IRequest<Response>
    {
        public int PageNumber { get; set; }
       
    }
}
