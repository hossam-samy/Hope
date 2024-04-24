using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.AddAdminRoleToUser
{
    internal class AddAdminRoleToUserCommandHandler : IRequestHandler<AddAdminRoleToUserCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<AddAdminRoleToUserCommandHandler> localizer;

        public AddAdminRoleToUserCommandHandler(UserManager<User> userManager, IStringLocalizer<AddAdminRoleToUserCommandHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(AddAdminRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                return await Response.FailureAsync("This user is already Admin");
            }

            await userManager.AddToRoleAsync(user, "Admin");

            return await Response.SuccessAsync(localizer["Success"]);
        }

    }
}

