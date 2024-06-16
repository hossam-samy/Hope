using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Commands.UnPinPost
{
    public class UnPinPostCommand:IRequest<Response>
    {
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
