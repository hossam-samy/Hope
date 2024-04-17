using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetPostsByUserId
{
    internal class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetPostsByUserIdQueryHandler> localizer;
        private readonly IHttpContextAccessor accessor;
        public GetPostsByUserIdQueryHandler(UserManager<User> userManager, IStringLocalizer<GetPostsByUserIdQueryHandler> localizer, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.accessor = accessor;
        }

        public async Task<Response> Handle(GetPostsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var userid=accessor?.HttpContext?.User.Claims.FirstOrDefault(i=>i.Type=="uid")?.Value;
            var user = await userManager.FindByIdAsync(userid!);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }


            List<GetAllPostsQueryResponse> allposts = [.. user.lostPeople?.Adapt<List<GetAllPostsQueryResponse>>(), .. user.lostThings?.Adapt<List<GetAllPostsQueryResponse>>()];

            return await Response.SuccessAsync(allposts, localizer["Success"].Value);

        }
    }
}
