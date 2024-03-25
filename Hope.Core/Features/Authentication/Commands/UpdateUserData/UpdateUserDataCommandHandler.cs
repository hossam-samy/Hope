using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.UpdateUserData
{
    internal class UpdateUserDataCommandHandler : IRequestHandler<UpdateUserDataCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UpdateUserDataCommandHandler> localizer;
        public UpdateUserDataCommandHandler(UserManager<User> userManager, IUnitofWork unitofWork, IStringLocalizer<UpdateUserDataCommandHandler> localizer)
        {
            this.userManager = userManager;
            work = unitofWork;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(UpdateUserDataCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId);

            if (user == null)
                return await Response.FailureAsync(localizer["Faild"].Value);



            user.DisplayName = command.DisplayName;
            user.City = command.City;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);

        }
    }
}
