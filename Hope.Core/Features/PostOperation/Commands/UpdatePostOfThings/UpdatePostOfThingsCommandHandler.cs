using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.UpdatePostOfThings
{
    internal class UpdatePostOfThingsCommandHandler : IRequestHandler<UpdatePostOfThingsCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UpdatePostOfThingsCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IMediaService mediaService;
        private readonly IHttpContextAccessor accessor;
        public UpdatePostOfThingsCommandHandler(IUnitofWork work, IStringLocalizer<UpdatePostOfThingsCommandHandler> localizer, UserManager<User> userManager, IMediaService mediaService, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.mediaService = mediaService;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(UpdatePostOfThingsCommand command, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            var user = await userManager.FindByIdAsync(userid!);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            }

            var post = user.lostThings.FirstOrDefault(i => i.Id == command.Id);

            if (post == null) return await Response.FailureAsync(localizer["BlockUpdatingPost"].Value);



            post.IsSearcher = command.IsSearcher ?? post.IsSearcher;
            post.Type = command.Type ?? post.Type;
            post.City = command.City ?? post.City;
            post.Description = command.Description ?? post.Description;

            var newUrl = await mediaService.UpdateFileAsync(post.ImageUrl, command.Image, "PostOfLostPeople", post.Id.ToString());

            post.ImageUrl = newUrl == string.Empty ? post.ImageUrl : newUrl;
            post.MissigDate = command.MissigDate ?? post.MissigDate;
            post.PhoneNumber = command.PhoneNumber ?? post.PhoneNumber;
            post.Town = command.Town ?? post.Town;



            await work.Repository<PostOfLostThings>().Update(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
