using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Features.Authentication.Commands.Register;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.RegisterAsAdmin
{
    internal class RegisterAsAdminCommandHandler : IRequestHandler<RegisterAsAdminCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<RegisterAsAdminCommandHandler> localizer;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IValidator<RegisterAsAdminCommand> validator;

        public RegisterAsAdminCommandHandler(UserManager<User> userManager, IStringLocalizer<RegisterAsAdminCommandHandler> localizer, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterAsAdminCommand> validator)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.validator = validator;
        }

        public async Task<Response> Handle(RegisterAsAdminCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }
            
            var user=command.Adapt<User>();    

            var result = await userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
                return await Response.FailureAsync(localizer["InvalidUserName"]);

            await userManager.AddToRoleAsync(user, "Admin");
            
            var jwtsecuritytoken = await jwtTokenGenerator.GenerateToken(user);

            return await Response.SuccessAsync(new RegisterCommandResponse
            {
                Id = user.Id,
                Email = user.Email,
                IsAuthenticated = true,
                Username = user.UserName,
                Roles = new List<string> {"Admin"},
                Token = jwtsecuritytoken,
                Name = user.DisplayName,
            }, localizer["Success"].Value);
        }
    }
}

