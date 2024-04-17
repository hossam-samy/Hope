using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Commands.HidePosts
{
    public class HidePostsCommand:IRequest<Response>
    {       
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
