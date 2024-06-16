using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Queries.GetProfile
{
    internal class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetProfileQueryHandler> localizer;
        private readonly IHttpContextAccessor accessor;

        public GetProfileQueryHandler(UserManager<User> userManager, IStringLocalizer<GetProfileQueryHandler> localizer, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(GetProfileQuery query, CancellationToken cancellationToken)
        {
            var userId = accessor.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value ?? Guid.NewGuid().ToString();
           
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

        
           // List<GetAllPostsQueryResponse> allposts = [..   user.lostPeople.Adapt<List<GetAllPostsQueryResponse>>(), .. user.lostThings.Adapt<List<GetAllPostsQueryResponse>>()];


            return await Response.SuccessAsync(new { user.DisplayName, user.UserName, user.UserImage, user.City,user.Email,user.PhoneNumber }, localizer["Success"].Value);

        }
    }
}
