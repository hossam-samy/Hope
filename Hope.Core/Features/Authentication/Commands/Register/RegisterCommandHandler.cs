using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Features.Authentication.Queries.Login;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response>
    {
        private readonly UserManager<User> userManager;

        private readonly IMapper mapper;
        private readonly IStringLocalizer<RegisterCommandHandler> localizer;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IValidator<RegisterCommand> validator;

        public RegisterCommandHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<RegisterCommandHandler> localizer, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterCommand> validator)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.localizer = localizer;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.validator = validator;

        }

        public async Task<Response> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var user = mapper.Map<User>(command);



            var asd= await userManager.CreateAsync(user, command.Password);

           

             await userManager.AddToRoleAsync(user, "User");

           
            var jwtsecuritytoken = await jwtTokenGenerator.GenerateToken(user);


            return await Response.SuccessAsync(new RegisterCommandResponse
            {
                Id = user.Id,
                Email = user.Email,
                IsAuthenticated = true,
                Username = user.UserName,
                Roles = new List<string> { "User" },
                Token = jwtsecuritytoken,
                Name = user.DisplayName,
            }, localizer["Success"].Value);
        }
    }
}
