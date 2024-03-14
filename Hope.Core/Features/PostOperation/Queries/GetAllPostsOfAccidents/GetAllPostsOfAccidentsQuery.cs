﻿using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents
{
    public class GetAllPostsOfAccidentsQuery : IRequest<Response>
    {
        public int? cursor { get; set; }
        public string UserId { get; set; }
       
    }
}