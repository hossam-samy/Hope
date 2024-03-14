using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Core.Service;
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

        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<AuthService> localizer;
        private readonly IUnitofWork unitofWork;
        private readonly IMediaService mediaService;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public RegisterCommandHandler(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, IStringLocalizer<AuthService> localizer, IUnitofWork unitofWork, IMediaService mediaService, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
            this.localizer = localizer;
            this.unitofWork = unitofWork;
            this.mediaService = mediaService;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Response> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (command.Password != command.ConfirmPassword)
                return await Response.FailureAsync(localizer["Password"].Value);


            if (await userManager.FindByEmailAsync(command.Email) is not null)
                return await Response.FailureAsync(localizer["EmailExist"].Value);



            var user = mapper.Map<User>(command);



            var result = await userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors, localizer["Faild"].Value);
            }

            var res = await userManager.AddToRoleAsync(user, "User");

            if (!res.Succeeded)
            {
                return await Response.FailureAsync(result.Errors, localizer["Faild"].Value);
            }
            var jwtsecuritytoken = await jwtTokenGenerator.GenerateToken(user);


            return await Response.SuccessAsync(new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                IsAuthenticated = true,
                Username = user.UserName,
                Roles = new List<string> { "User" },
                Token = jwtsecuritytoken,
                Name = user.Name,
            }, localizer["Success"].Value);
        }
    }
}
