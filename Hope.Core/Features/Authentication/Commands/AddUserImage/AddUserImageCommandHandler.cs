using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.AddUserImage
{
    internal class AddUserImageCommandHandler : IRequestHandler<AddUserImageCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<AddUserImageCommandHandler> localizer;
        private readonly IUnitofWork unitofWork;
        private readonly IMediaService mediaService;
        private readonly IValidator<AddUserImageCommand> validator;

        public AddUserImageCommandHandler(UserManager<User> userManager, IStringLocalizer<AddUserImageCommandHandler> localizer, IUnitofWork unitofWork, IMediaService mediaService, IValidator<AddUserImageCommand> validator)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.unitofWork = unitofWork;
            this.mediaService = mediaService;
            this.validator = validator;
        }
        public async Task<Response> Handle(AddUserImageCommand command, CancellationToken cancellationToken)
        {
            var result=await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(x => x.ErrorMessage), localizer["Faild"]);
            }

            var user = await userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"]);
            }

            user.UserImage = await mediaService.AddFileAsync(command.Image, "User", user.Id);

            await unitofWork.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
