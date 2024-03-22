using FluentValidation;
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
        private readonly IValidator<HidePostsCommand> validator;
        public HidePostHandler(IUnitofWork work, IStringLocalizer<HidePostHandler> localizer, UserManager<User> userManager, IValidator<HidePostsCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
        }
        public async Task<Response> Handle(HidePostsCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }
            if (command.IsPeople)
                return await HidePost<PostOfLostPeople>(command.UserId!, command.PostId);
            else
                return await HidePost<PostOfLostThings>(command.UserId!, command.PostId);

        }
        public async Task<Response> HidePost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);


            Post? post =  await work.Repository<T>().GetItem(i => i.Id == PostId);
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"].Value);

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

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
