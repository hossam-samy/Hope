using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.AdminOperation.Commands.DeletePost
{
    public class AdminDeletePostCommand:IRequest<Response>
    {
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
