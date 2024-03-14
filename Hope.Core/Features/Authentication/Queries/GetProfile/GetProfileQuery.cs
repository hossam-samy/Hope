using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.GetProfile
{
    public class GetProfileQuery:IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
