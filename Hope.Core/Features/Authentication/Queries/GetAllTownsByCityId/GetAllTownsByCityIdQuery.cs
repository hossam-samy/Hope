using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.GetAllTownsByCityId
{
    public class GetAllTownsByCityIdQuery:IRequest<Response>
    {
        public int Id { get; set; }
    }
}
