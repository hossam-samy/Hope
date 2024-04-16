using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Commands.UnHidePost
{
    public class UnHidePostCommand:IRequest<Response>
    {
        public string? UserId { get; set; }
        public int PostId { get; set; }
        public bool IsPeople { get; set; }
    }
}
