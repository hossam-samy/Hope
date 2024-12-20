﻿using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Queries.GetPinnedPostsByUserId
{
    public class GetPinnedPostsByUserIdQuery : IRequest<Response>
    {
        public string? UserId { get; set; }

    }
}
