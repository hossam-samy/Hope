using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
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


            var posts =  work.Repository<PostOfLostThings>().
                Get(i => !i.HiddenThings.Contains(user), new[] { "HiddenThings" })
                .Result.Skip((query.PageNumber - 1) * 32).Take(32).ToList().Adapt<List<GetAllPostsOfThingsQueryResponse>>() ;



            return await Response.SuccessAsync( posts, localizer["Success"].Value);
        }
    }
}
