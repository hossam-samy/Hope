using Hope.Core.Common;
using Hope.Core.Common.Consts;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfThings
{
    internal class GetAllPostsOfThingsQueryHandler : IRequestHandler<GetAllPostsOfThingsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllPostsOfThingsQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        public GetAllPostsOfThingsQueryHandler(IUnitofWork work, IStringLocalizer<GetAllPostsOfThingsQueryHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(GetAllPostsOfThingsQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId);

            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var posts = query.cursor != 0 ? work.Repository<PostOfLostThings>().
                Get(i => i.Id > query.cursor && !i.HiddenThings.Contains(user), new[] { "HiddenThings" }).Result.Take(32).ToList().Adapt<List<PostThingResponse>>() : new List<PostThingResponse>();


            query.cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts, query.cursor }, localizer["Success"].Value);
        }
    }
}
