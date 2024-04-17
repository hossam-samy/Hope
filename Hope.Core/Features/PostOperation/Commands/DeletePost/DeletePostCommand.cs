using Hope.Core.Common;
using MediatR;
using System.Runtime.Serialization;

namespace Hope.Core.Features.PostOperation.Commands.DeletePost
{
    public class DeletePostCommand:IRequest<Response>
    {

        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
