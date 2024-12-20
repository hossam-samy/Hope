﻿using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetArchivedPosts
{
    internal class GetArchivedPostsQueryHandler : IRequestHandler<GetArchivedPostsQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetArchivedPostsQueryHandler> localizer;
        private readonly IHttpContextAccessor accessor;

        public GetArchivedPostsQueryHandler(UserManager<User> userManager, IStringLocalizer<GetArchivedPostsQueryHandler> localizer, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.accessor = accessor;
        }

        public async Task<Response> Handle(GetArchivedPostsQuery query, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;

            var user = await userManager.FindByIdAsync(userid!);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

            var peopleposts = user?.HiddingPeoples?.Adapt<List<GetAllPostsQueryResponse>>();


            var thingsposts = user?.HiddingThings?.Adapt<List<GetAllPostsQueryResponse>>();




            List<GetAllPostsQueryResponse> allposts = [.. peopleposts, .. thingsposts];


            return await Response.SuccessAsync(allposts, localizer["Success"].Value);

        }
    }
}
