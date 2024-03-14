using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Commands.UnPinPost
{
    public class UnPinPostCommand:IRequest<Response>
    {
        public string? UserId { get; set; }
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
