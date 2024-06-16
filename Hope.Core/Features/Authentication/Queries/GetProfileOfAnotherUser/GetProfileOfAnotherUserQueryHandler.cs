using Hope.Core.Common;
using Hope.Core.Features.Authentication.Queries.GetProfile;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.GetProfileOfAnotherUser
{
    internal class GetProfileOfAnotherUserQueryHandler : IRequestHandler<GetProfileOfAnotherUserQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetProfileOfAnotherUserQueryHandler> localizer;

        public GetProfileOfAnotherUserQueryHandler(UserManager<User> userManager, IStringLocalizer<GetProfileOfAnotherUserQueryHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }
        public async Task<Response> Handle(GetProfileOfAnotherUserQuery query, CancellationToken cancellationToken)
        {
          
            var user = await userManager.FindByIdAsync(query.UserId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }


            // List<GetAllPostsQueryResponse> allposts = [..   user.lostPeople.Adapt<List<GetAllPostsQueryResponse>>(), .. user.lostThings.Adapt<List<GetAllPostsQueryResponse>>()];


            return await Response.SuccessAsync(new { user.DisplayName, user.UserName, user.UserImage, user.City, user.Email, user.PhoneNumber }, localizer["Success"].Value);

        }
    }
}
