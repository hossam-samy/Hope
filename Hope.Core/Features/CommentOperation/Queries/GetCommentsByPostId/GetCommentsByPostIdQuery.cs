using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQuery:IRequest<Response>
    {
          public bool IsPeople { get; set; }

          public int PostId { get; set; }   
    }
}
