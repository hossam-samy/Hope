using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Queries.GetAllDeletedPosts
{
    public class GetAllDeletedPostsQuery:IRequest<Response>
    {
    }

    public class GetAllDeletedPostsQueryHandler : IRequestHandler<GetAllDeletedPostsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllDeletedPostsQueryHandler> localizer;

        public GetAllDeletedPostsQueryHandler(IUnitofWork work, IStringLocalizer<GetAllDeletedPostsQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }


        public async Task<Response> Handle(GetAllDeletedPostsQuery request, CancellationToken cancellationToken)
        {
          var PeopleDeletedPosts = work.Repository<PostOfLostPeople>().IgnoreFilter().Result.Where(i => i.IsDeleted).ToList().Adapt<List<GetAllPostsQueryResponse>>();
          
          var thingsDeletedPosts = work.Repository<PostOfLostThings>().IgnoreFilter().Result.Where(i => i.IsDeleted).ToList().Adapt<List<GetAllPostsQueryResponse>>();


          return await Response.SuccessAsync(new { PeopleDeletedPosts, thingsDeletedPosts }, localizer["Success"]);

        }
    }
}
