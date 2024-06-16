using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Queries.GetCountOfCreatedPosts
{
    internal class AdminGetCountOfCreatedPostsQueryHandler : IRequestHandler<AdminGetCountOfCreatedPostsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<AdminGetCountOfCreatedPostsQueryHandler> localizer;

        public AdminGetCountOfCreatedPostsQueryHandler(IUnitofWork work, IStringLocalizer<AdminGetCountOfCreatedPostsQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(AdminGetCountOfCreatedPostsQuery query, CancellationToken cancellationToken)
        {
            var now=DateTime.Now;

            var prevMounth = now.Month == 1 ? 12 : now.Month - 1;
            var prevYear = now.Month== 1 ? now.Year-1 : now.Year;

            var PeoplepostsCount =  work.Repository<PostOfLostPeople>().Get(i => i.CreationDate.Month == now.Month && i.CreationDate.Year == now.Year).Result.Count();
            var ThingpostsCount =  work.Repository<PostOfLostThings>().Get(i => i.CreationDate.Month == now.Month && i.CreationDate.Year == now.Year).Result.Count();

            var currentCount = PeoplepostsCount + ThingpostsCount;

            var PrevPeoplepostsCount =  work.Repository<PostOfLostPeople>().Get(i => i.CreationDate.Month ==prevMounth && i.CreationDate.Year == prevYear).Result.Count();
            var PrevThingpostsCount =  work.Repository<PostOfLostThings>().Get(i => i.CreationDate.Month == prevMounth && i.CreationDate.Year == prevYear).Result.Count();

            var prevCount=PrevPeoplepostsCount + PrevThingpostsCount;   

            var advancePercentage=currentCount>prevCount?currentCount*100/ (prevCount == 0 ? 1 : prevCount) : currentCount * 100 / (prevCount==0?1:prevCount)*-1 ;


            return await Response.SuccessAsync(new { currentCount, advancePercentage }, localizer["Success"]);
        }
    }
}
