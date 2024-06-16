using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfLosties
{
    public class GetAllPostsOfLostiesQuery:IRequest<Response>
    {
        public int PageNumber { get; set; }
       
    }
}
