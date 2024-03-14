using Hope.Core.Common;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.AddUserImage
{
    internal class AddUserImageCommandHandler : IRequestHandler<AddUserImageCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<AuthService> localizer;
        private readonly IUnitofWork unitofWork;
        private readonly IMediaService mediaService;

        public AddUserImageCommandHandler(UserManager<User> userManager, IStringLocalizer<AuthService> localizer, IUnitofWork unitofWork, IMediaService mediaService)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.unitofWork = unitofWork;
            this.mediaService = mediaService;
        }
        public async Task<Response> Handle(AddUserImageCommand command, CancellationToken cancellationToken)
        {
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
