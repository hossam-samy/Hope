using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.Authentication.Queries.GetProfileOfAnotherUser
{
    public class GetProfileOfAnotherUserQuery:IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
