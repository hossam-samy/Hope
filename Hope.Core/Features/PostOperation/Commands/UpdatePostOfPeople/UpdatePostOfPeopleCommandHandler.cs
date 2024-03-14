using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.UpdatePostOfPeople
{
    internal class UpdatePostOfPeopleCommandHandler : IRequestHandler<UpdatePostOfPeopleCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UpdatePostOfPeopleCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IMediaService mediaService;
        public UpdatePostOfPeopleCommandHandler(IUnitofWork work, IStringLocalizer<UpdatePostOfPeopleCommandHandler> localizer, UserManager<User> userManager, IMediaService mediaService)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.mediaService = mediaService;
        }
        public async Task<Response> Handle(UpdatePostOfPeopleCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var post = user.lostPeople.FirstOrDefault(i => i.Id == command.Id);

            if (post == null) return await Response.FailureAsync(localizer["BlockUpdatingPost"]);


            post.IsSearcher = command.IsSearcher ?? post.IsSearcher;
            post.Age = command.Age ?? post.Age;
            post.Name = command .Name ?? post.Name;
            post.City = command.City ?? post.City;
            post.Condition = command.Condition ?? post.Condition;
            post.Description = command.Description ?? post.Description;

            var newUrl = await mediaService.UpdateFileAsync(post.ImageUrl, command.Image, "PostOfLostPeople", post.Id.ToString());

            post.ImageUrl = newUrl == string.Empty ? post.ImageUrl : newUrl;
            post.MissigDate = command.MissigDate ?? post.MissigDate;
            post.PhoneNumber = command.PhoneNumber ?? post.PhoneNumber;
            post.Town = command.Town ?? post.Town;
            post.Gendre = command.Gendre ?? post.Gendre;

            await work.Repository<PostOfLostPeople>().Update(post);



            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
