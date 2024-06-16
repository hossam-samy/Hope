using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Queries.GetRecommendedPosts
{
    public class GetRecommendedPostsQuery:IRequest<Response>
    {
        public string City { get; set; }
    }
}
