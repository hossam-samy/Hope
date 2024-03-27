using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.GetAllTownsByCityId
{
    internal class GetAllTownsByCityIdQueryHandler : IRequestHandler<GetAllTownsByCityIdQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllTownsByCityIdQueryHandler> localizer;

        public GetAllTownsByCityIdQueryHandler(IUnitofWork work, IStringLocalizer<GetAllTownsByCityIdQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetAllTownsByCityIdQuery query, CancellationToken cancellationToken)
        {
            
            var city=await work.Repository<Cities>().GetItem(i=>i.Id==query.Id); 

            return await Response.SuccessAsync(city.Towns, localizer["Success"].Value);  
        }
    }
}
