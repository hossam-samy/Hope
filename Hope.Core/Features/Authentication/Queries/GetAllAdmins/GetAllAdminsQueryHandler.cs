using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Queries.GetAllAdmins
{
    internal class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetAllAdminsQueryHandler> localizer;
        public GetAllAdminsQueryHandler(UserManager<User> userManager, IStringLocalizer<GetAllAdminsQueryHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var admins = userManager.GetUsersInRoleAsync("Admin").Result.Select(i => new {i.DisplayName,i.UserImage,i.Email,i.Id});

            return await Response.SuccessAsync(admins, localizer["Success"]); 
        }
    }
}
