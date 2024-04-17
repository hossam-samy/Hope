using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Commands.PinPost
{
    public class PinPostCommand:IRequest<Response>
    {
        public int PostId { get; set; }
        public bool IsPeople { get; set; }

        
    }
}
