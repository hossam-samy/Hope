using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.HidePosts
{
    internal class HidePostHandler : IRequestHandler<HidePostsCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<HidePostHandler> localizer;
        private readonly UserManager<User> userManager;
        public HidePostHandler(IUnitofWork work, IStringLocalizer<HidePostHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(HidePostsCommand command, CancellationToken cancellationToken)
        {
            if (command.IsPeople)
                return await HidePost<PostOfLostPeople>(command.UserId, command.PostId);
            else
                return await HidePost<PostOfLostThings>(command.UserId, command.PostId);

        }
        public async Task<Response> HidePost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            Post? post = work.Repository<T>().Get(i => i.Id == PostId).Result.FirstOrDefault();
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"]);

            }


            if (user.PinningPeoples.Any(i => i == post) || user.PinningThings.Any(i => i == post))
            {

                return await Response.FailureAsync("HideError");
            }


            if (typeof(T).Name == nameof(PostOfLostPeople))
            {

                user.HiddingPeoples.Add((PostOfLostPeople)post);

            }
            else
            {

                user.HiddingThings.Add((PostOfLostThings)post);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
