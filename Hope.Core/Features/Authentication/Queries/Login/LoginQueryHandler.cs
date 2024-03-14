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
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<AuthService> localizer;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
       

        public LoginQueryHandler(UserManager<User> userManager, IStringLocalizer<AuthService> localizer, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<Response> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(query.Email!);


            if (user is null || !await userManager.CheckPasswordAsync(user, query.Password!))
            {
                return await Response.FailureAsync(localizer["InvalidLogin"].Value);
            }

            var token = await jwtTokenGenerator.GenerateToken(user);

            var role = await userManager.GetRolesAsync(user);

            var login = new LoginResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = token,
                Roles = role.ToList(),
                Name = user.Name,
                IsAuthenticated = true,
            };

            return await Response.SuccessAsync(login, localizer["Success"].Value);

        }
    }
}
