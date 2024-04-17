using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfThings
{
    internal class GetAllPostsOfThingsQueryHandler : IRequestHandler<GetAllPostsOfThingsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllPostsOfThingsQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor accessor;
        public GetAllPostsOfThingsQueryHandler(IUnitofWork work, IStringLocalizer<GetAllPostsOfThingsQueryHandler> localizer, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(GetAllPostsOfThingsQuery query, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            var user = await userManager.FindByIdAsync(userid);

            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);


            var posts =  work.Repository<PostOfLostThings>().
                Get(i => !i.HiddenThings.Contains(user), new[] { "HiddenThings" })
                .Result.Skip((query.PageNumber - 1) * 32).Take(32).ToList().Adapt<List<GetAllPostsOfThingsQueryResponse>>() ;



            return await Response.SuccessAsync( posts, localizer["Success"].Value);
        }
    }
}
