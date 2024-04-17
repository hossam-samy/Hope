using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.UpdateUserData
{
    internal class UpdateUserDataCommandHandler : IRequestHandler<UpdateUserDataCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UpdateUserDataCommandHandler> localizer;
        private readonly IHttpContextAccessor accessor;
        public UpdateUserDataCommandHandler(UserManager<User> userManager, IUnitofWork unitofWork, IStringLocalizer<UpdateUserDataCommandHandler> localizer, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            work = unitofWork;
            this.localizer = localizer;
            this.accessor = accessor;
        }

        public async Task<Response> Handle(UpdateUserDataCommand command, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            var user = await userManager.FindByIdAsync(userid!);

            if (user == null||!await userManager.CheckPasswordAsync(user,command.Password))
                return await Response.FailureAsync(localizer["WrongPassword"].Value);



            user.DisplayName = command.DisplayName;
            user.City = command.City;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);

        }
    }
}
