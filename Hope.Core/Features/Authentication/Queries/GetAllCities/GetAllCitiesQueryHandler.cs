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

namespace Hope.Core.Features.Authentication.Queries.GetAllCities
{
    internal class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllCitiesQueryHandler> localizer;

        public GetAllCitiesQueryHandler(IUnitofWork work, IStringLocalizer<GetAllCitiesQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await work.Repository<Cities>().Get(i => new { i.Id,i.Name});

            return await Response.SuccessAsync(cities, localizer["Success"].Value);
            
        }
    }
}
