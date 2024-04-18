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
        public GetPinnedPostsByUserIdQueryHandler(UserManager<User> userManager, IStringLocalizer<GetPinnedPostsByUserIdQueryHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetPinnedPostsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }


            List<GetAllPostsQueryResponse> allposts = [.. user.PinningPeoples?.Adapt<List<GetAllPostsQueryResponse>>(), .. user.PinningThings?.Adapt<List<GetAllPostsQueryResponse>>()];

            return await Response.SuccessAsync(allposts, localizer["Success"].Value);

        }
    }
}
