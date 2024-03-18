using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.PinPost
{
    internal class PinPostCommandHandler : IRequestHandler<PinPostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<PinPostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        public PinPostCommandHandler(IUnitofWork work, IStringLocalizer<PinPostCommandHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(PinPostCommand command, CancellationToken cancellationToken)
        {
           if(command.IsPeople) 
                return await PinPost<PostOfLostPeople>(command.UserId,command.PostId);
            else
                return await PinPost<PostOfLostThings>(command.UserId,command.PostId);

        }
        private async Task<Response> PinPost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            }


            Post? post = work.Repository<T>().Get(i => i.Id == PostId).Result.FirstOrDefault();
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"].Value);

            }
            if (typeof(T).Name == nameof(PostOfLostPeople))
            {

                user.PinningPeoples.Add((PostOfLostPeople)post);

            }
            else
            {

                user.PinningThings.Add((PostOfLostThings)post);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
