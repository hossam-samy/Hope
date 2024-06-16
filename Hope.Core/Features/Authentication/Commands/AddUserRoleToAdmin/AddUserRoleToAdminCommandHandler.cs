using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.AddUserRoleToAdmin
{
    internal class AddUserRoleToAdminCommandHandler : IRequestHandler<AddUserRoleToAdminCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<AddUserRoleToAdminCommandHandler> localizer;

        public AddUserRoleToAdminCommandHandler(UserManager<User> userManager, IStringLocalizer<AddUserRoleToAdminCommandHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(AddUserRoleToAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

            if (await userManager.IsInRoleAsync(user, "User"))
            {
                return await Response.FailureAsync("This user is already User");
            }

            await userManager.AddToRoleAsync(user, "User");

            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
