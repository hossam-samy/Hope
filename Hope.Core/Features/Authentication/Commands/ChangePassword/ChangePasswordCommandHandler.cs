using FluentValidation;
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
        private readonly IValidator<ChangePasswordCommand> validator;


        public ChangePasswordCommandHandler(UserManager<User> userManager, IStringLocalizer<ChangePasswordCommandHandler> localizer, IValidator<ChangePasswordCommand> validator)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.validator = validator;
        }
        public async Task<Response> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {

            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }


            var user = await userManager.FindByEmailAsync(command.UserEmail);


            if (user is null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);


             await userManager.RemovePasswordAsync(user);

            await userManager.AddPasswordAsync(user, command.password);


            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
