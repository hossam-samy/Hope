using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.UnPinPost
{
    internal class UnPinPostCommandHandler : IRequestHandler<UnPinPostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UnPinPostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        public UnPinPostCommandHandler(IUnitofWork work, IStringLocalizer<UnPinPostCommandHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(UnPinPostCommand command, CancellationToken cancellationToken)
        {
            if (command.IsPeople)
                return await UnPinPost<PostOfLostPeople>(command.UserId, command.PostId);
            else
                return await UnPinPost<PostOfLostThings>(command.UserId, command.PostId);
        }
        private async Task<Response> UnPinPost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }


            Post? post = await work.Repository<T>().GetItem(i => i.Id == PostId);
            if (post == null)
            {
                return await Response.FailureAsync(localizer["PostNotExist"].Value);
            }

            if (!user.PinningPeoples.Any(i => i == post) && !user.PinningThings.Any(i => i == post))
            {
                return await Response.FailureAsync(localizer["UnPinError"].Value);
            }

            if (typeof(T).Name == nameof(PostOfLostPeople))
            {
                user.PinningPeoples.Remove((PostOfLostPeople)post);
            }
            else
            {
                user.PinningThings.Remove((PostOfLostThings)post);
            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
