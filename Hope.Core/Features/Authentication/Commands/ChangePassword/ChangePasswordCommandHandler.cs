using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<ChangePasswordCommandHandler> localizer;
        

        public ChangePasswordCommandHandler(UserManager<User> userManager, IStringLocalizer<ChangePasswordCommandHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            
        }
        public async Task<Response> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(command.UserEmail);


            if (user is null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var result = await userManager.RemovePasswordAsync(user);


            if (!result.Succeeded)
                return await Response.FailureAsync(localizer["Faild"]);


            var identityResult = await userManager.AddPasswordAsync(user, command.password);


            if (!identityResult.Succeeded)
                return await Response.FailureAsync(localizer["Faild"]);

            return await Response.SuccessAsync("good");
        }
    }
}
