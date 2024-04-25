using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Hope.Core.Features.AdminOperation.Queries.GetNumberOfNewAccountsForWeek
{
    public class GetNumberOfNewAccountsForWeekQuery:IRequest<Response>
    {
    }
    public class GetNumberOfNewAccountsForWeekQueryHandler : IRequestHandler<GetNumberOfNewAccountsForWeekQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetNumberOfNewAccountsForWeekQueryHandler> localizer;

        public GetNumberOfNewAccountsForWeekQueryHandler(IUnitofWork work, IStringLocalizer<GetNumberOfNewAccountsForWeekQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetNumberOfNewAccountsForWeekQuery query, CancellationToken cancellationToken)
        {
            var now=DateTime.Now;


            List<DateTime> days = [];

            for (int i = 0; i < 7; i++)
            {
                days.Add(now);  
                now = now.Subtract(TimeSpan.FromDays(1));
            }
            days.Reverse();

            var users = await work.Repository<User>().Get(i => i.CreationDate.Date >= days[0] && i.CreationDate.Date <= days[6]);
            List<int> response = new List<int>();

            for(int j=0;j<7;j++) { 
                response.Add(users.Where(i => i.CreationDate.Day == days[j].Day).Count());
            }
           

            return await Response.SuccessAsync(response, localizer["Success"].Value);   

        }
    }
}
