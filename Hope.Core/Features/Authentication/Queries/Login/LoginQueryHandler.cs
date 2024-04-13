using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<LoginQueryHandler> localizer;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IValidator<LoginQuery> validator;


        public LoginQueryHandler(UserManager<User> userManager, IStringLocalizer<LoginQueryHandler> localizer, IJwtTokenGenerator jwtTokenGenerator, IValidator<LoginQuery> validator)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.validator = validator;
        }
        public async Task<Response> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var result=await validator.ValidateAsync(query); 

            if(!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var user = await userManager.FindByEmailAsync(query.Email!);

           


            if (user is null || !await userManager.CheckPasswordAsync(user, query.Password!))
            {
                return await Response.FailureAsync(localizer["InvalidLogin"].Value);
            }

            var token = await jwtTokenGenerator.GenerateToken(user);

          

            var role = await userManager.GetRolesAsync(user);

            var login = new LoginQueryResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = token,
                Roles = role.ToList(),
                Name = user.DisplayName,
                UserImage=user.UserImage,
                IsAuthenticated = true,
            };

            return await Response.SuccessAsync(login, localizer["Success"].Value);

        }
    }
}
