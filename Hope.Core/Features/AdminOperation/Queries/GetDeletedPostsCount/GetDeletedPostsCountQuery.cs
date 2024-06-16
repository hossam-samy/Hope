using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Queries.GetDeletedPostsCount
{
    public class GetDeletedPostsCountQuery:IRequest<Response>
    {
    }
    public class GetDeletedPostsCountQueryHandler : IRequestHandler<GetDeletedPostsCountQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetDeletedPostsCountQueryHandler> localizer;


        public GetDeletedPostsCountQueryHandler(IUnitofWork work, IStringLocalizer<GetDeletedPostsCountQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetDeletedPostsCountQuery query, CancellationToken cancellationToken)
        {
            var deletedPeoplePostsCount =  work.Repository<PostOfLostPeople>().IgnoreFilter().Result.Count(i=>i.IsDeleted);
            var deletedthingsPostsCount =  work.Repository<PostOfLostThings>().IgnoreFilter().Result.Count(i=>i.IsDeleted);


            return await Response.SuccessAsync(deletedthingsPostsCount + deletedPeoplePostsCount, localizer["Success"]);
        }
    }
}
