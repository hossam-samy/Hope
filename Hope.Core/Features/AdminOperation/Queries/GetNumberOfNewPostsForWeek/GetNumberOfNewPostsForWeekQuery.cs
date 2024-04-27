using Hope.Core.Common;
using Hope.Core.Features.AdminOperation.Queries.GetNumberOfNewAccountsForWeek;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.AdminOperation.Queries.GetNumberOfNewPostsForWeek
{
    public class GetNumberOfNewPostsForWeekQuery:IRequest<Response>
    {
    }

    public class GetNumberOfNewPostsForWeekQueryHandler : IRequestHandler<GetNumberOfNewPostsForWeekQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetNumberOfNewPostsForWeekQueryHandler> localizer;

        public GetNumberOfNewPostsForWeekQueryHandler(IUnitofWork work, IStringLocalizer<GetNumberOfNewPostsForWeekQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }
        public async Task<Response> Handle(GetNumberOfNewPostsForWeekQuery query, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;


            List<DateTime> days = [];

            for (int i = 0; i < 7; i++)
            {
                days.Add(now);
                now = now.Subtract(TimeSpan.FromDays(1));
            }
            days.Reverse();

            var PeoplePosts = await work.Repository<PostOfLostPeople>().Get(i => i.CreationDate.Date >= days[0] && i.CreationDate.Date <= days[6]);
            
            var thingsPosts = await work.Repository<PostOfLostThings>().Get(i => i.CreationDate.Date >= days[0] && i.CreationDate.Date <= days[6]);
            
            List<int> response = [];

            for (int j = 0; j < 7; j++)
            {
                response.Add(PeoplePosts.Where(i => i.CreationDate.Day == days[j].Day).Count()+ thingsPosts.Where(i => i.CreationDate.Day == days[j].Day).Count());
            }


            return await Response.SuccessAsync(response, localizer["Success"].Value);
        }
    }
}
