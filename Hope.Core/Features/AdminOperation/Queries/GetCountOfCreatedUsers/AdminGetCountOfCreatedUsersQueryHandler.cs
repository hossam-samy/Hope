using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.AdminOperation.Queries.GetCountOfCreatedUsers
{
    internal class AdminGetCountOfCreatedUsersQueryHandler : IRequestHandler<AdminGetCountOfCreatedUsersQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<AdminGetCountOfCreatedUsersQueryHandler> localizer;

        public AdminGetCountOfCreatedUsersQueryHandler(IUnitofWork work, IStringLocalizer<AdminGetCountOfCreatedUsersQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(AdminGetCountOfCreatedUsersQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            var prevMounth = now.Month == 1 ? 12 : now.Month - 1;
            var prevYear = now.Month == 1 ? now.Year - 1 : now.Year;

            var usersCount = work.Repository<User>().Get(i => i.CreationDate.Month == now.Month && i.CreationDate.Year == now.Year).Result.Count();
           
            
            var prevUsersCount = work.Repository<User>().Get(i => i.CreationDate.Month == prevMounth && i.CreationDate.Year == prevYear).Result.Count();
           
            
             var advancePercentage = usersCount > prevUsersCount ? usersCount * 100 / (prevUsersCount == 0 ? 1 : prevUsersCount) : usersCount * 100 / (prevUsersCount==0?1:prevUsersCount) * (-1);


            return await Response.SuccessAsync(new { usersCount, advancePercentage }, localizer["Success"]);

        }
    }
}
