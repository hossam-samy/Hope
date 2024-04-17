using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetPinnedPostsByUserId
{
    internal class GetPinnedPostsByUserIdQueryHandler : IRequestHandler<GetPinnedPostsByUserIdQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetPinnedPostsByUserIdQueryHandler> localizer;
        private readonly IHttpContextAccessor accessor;
        public GetPinnedPostsByUserIdQueryHandler(UserManager<User> userManager, IStringLocalizer<GetPinnedPostsByUserIdQueryHandler> localizer, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.accessor = accessor;
        }

        public async Task<Response> Handle(GetPinnedPostsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            var user = await userManager.FindByIdAsync(userid!);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }


            List<GetAllPostsQueryResponse> allposts = [.. user.PinningPeoples?.Adapt<List<GetAllPostsQueryResponse>>(), .. user.PinningThings?.Adapt<List<GetAllPostsQueryResponse>>()];

            return await Response.SuccessAsync(allposts, localizer["Success"].Value);

        }
    }
}
