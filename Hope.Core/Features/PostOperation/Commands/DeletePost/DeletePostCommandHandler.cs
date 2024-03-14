using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.DeletePost
{
    internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<DeletePostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IMediaService mediaService;
        public DeletePostCommandHandler(IUnitofWork work, IStringLocalizer<DeletePostCommandHandler> localizer, UserManager<User> userManager, IMediaService mediaService)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.mediaService = mediaService;
        }
        public async Task<Response> Handle(DeletePostCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);
            }

            Post? post;
            if (command.IsPeople)
            {
                post = user.lostPeople.FirstOrDefault();
                if (post == null)
                {
                    return await Response.FailureAsync("You are not allowed to delete this post");

                }
                var peopleposts = (PostOfLostPeople)post;

                foreach (var item in peopleposts?.Comments)
                {
                    await DeleteComment(item.Id);
                }

                await mediaService.DeleteFileAsync(post.ImageUrl);
                await work.Repository<PostOfLostPeople>().Delete((PostOfLostPeople)post);
                return await Response.SuccessAsync(localizer["Success"]);
            }
            post = user.lostThings.FirstOrDefault();
            if (post == null)
            {
                return await Response.FailureAsync("PostNotExist");

            }
            var Thingsposts = (PostOfLostThings)post;

            foreach (var item in Thingsposts?.Comments)
            {
                await DeleteComment(item.Id);
            }
            await mediaService.DeleteFileAsync(post.ImageUrl);
            await work.Repository<PostOfLostThings>().Delete((PostOfLostThings)post);
            return await Response.SuccessAsync(localizer["Success"]);

        }
        private async Task DeleteComment(int id)
        {
            var comment = work.Repository<Comment>().Get(i => i.Id == id).Result.FirstOrDefault();

            await work.Repository<Comment>().Delete(comment);



        }
    }
}
