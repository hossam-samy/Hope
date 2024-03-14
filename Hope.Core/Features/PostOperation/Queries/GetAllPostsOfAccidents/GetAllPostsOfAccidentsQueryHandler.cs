using Hope.Core.Common;
using Hope.Core.Common.Consts;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents
{
    internal class GetAllPostsOfAccidentQueryHandler : IRequestHandler<GetAllPostsOfAccidentsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        public GetAllPostsOfAccidentQueryHandler(IUnitofWork work, IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(GetAllPostsOfAccidentsQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);

            var posts = query.cursor != 0 ? work.Repository<PostOfLostPeople>().
               Get(i => i.Id > query.cursor && i.Condition == Condition.accidents && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" })
               .Result.Take(32).ToList().Adapt<List<GetAllPostsOfPeopleQueryResponse>>() : new List<GetAllPostsOfPeopleQueryResponse>();

            query.cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts, query.cursor }, localizer["Success"].Value);
        }
    }
}
