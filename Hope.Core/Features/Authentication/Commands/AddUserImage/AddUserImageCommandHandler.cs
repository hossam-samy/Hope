using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor accessor;

        public AddUserImageCommandHandler(UserManager<User> userManager, IStringLocalizer<AddUserImageCommandHandler> localizer, IUnitofWork unitofWork, IMediaService mediaService, IValidator<AddUserImageCommand> validator, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.unitofWork = unitofWork;
            this.mediaService = mediaService;
            this.validator = validator;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(AddUserImageCommand command, CancellationToken cancellationToken)
        {
            var result=await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(x => x.ErrorMessage), localizer["Faild"].Value);
            }

            var user = await userManager.FindByIdAsync(accessor.HttpContext!.User.FindFirst("uid")!.Value);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

            user.UserImage = await mediaService.AddFileAsync(command.Image!, "User", user.Id);

            await unitofWork.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
